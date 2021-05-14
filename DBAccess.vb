Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration
Imports log4net
Imports MySql.Data.MySqlClient

Namespace DLL

    Friend Class getDBobject
        Private Log As ILog = LogManager.GetLogger(GetType(getDBobject))
        Dim _const As New Costanti()
        Dim myUtility As New AnagUtility

        Public Sub New()
        End Sub
#Region "Gestione di SQL o MySQL"
        Private m_Connection As String
        Private _TypeDB As String

        Public Sub New(TypeDB As String, ByVal _ConnectionString As String)
            'Se non si usa la stringa di connessione di  default del WebConfig
            m_Connection = _ConnectionString
            _TypeDB = TypeDB
        End Sub

        Private ReadOnly Property ToExecSP As String
            Get
                If (_TypeDB = "MySQL") Then
                    Return "CALL "
                Else
                    Return "EXEC "
                End If

            End Get
        End Property
        Private ReadOnly Property PrefVarSP As String
            Get
                If (_TypeDB = "MySQL") Then
                    Return "@var"
                Else
                    Return "@"
                End If
            End Get
        End Property
        Public Function GetSQL(ByVal sSQL As String, ParamArray ByVal myParam() As String) As String
            Dim sRet As String = ""
            For Each myItem As String In myParam
                If (sRet <> String.Empty) Then
                    sRet = sRet + ","
                End If
                sRet = sRet + PrefVarSP + myItem
            Next
            If (_TypeDB = "MySQL") Then
                sRet = ToExecSP + sSQL + " (" + sRet + ")"
            Else
                sRet = ToExecSP + sSQL + " " + sRet
            End If
            Return sRet
        End Function
        Public Function GetParam(ByVal Name As String, ByVal Value As Object) As Object
            If (_TypeDB = "MySQL") Then
                Return New MySqlParameter((PrefVarSP + Name), Value)
            Else
                Return New SqlParameter((PrefVarSP + Name), Value)
            End If
        End Function

        Public Function GetDataSet(sSQL As String, ByVal ParamArray commandParameters() As ArrayParam) As DataSet
            Dim myDataSet As New DataSet
            Dim sLog As String = ""

            If _TypeDB = Costanti.DBType_MySQL Then
                Dim cmdMyCommand As New MySqlCommand
                Try
                    cmdMyCommand = myUtility.InizializzaCmd(_TypeDB, m_Connection)
                    cmdMyCommand.CommandType = CommandType.StoredProcedure
                    cmdMyCommand.CommandText = sSQL
                    sLog = sSQL
                    cmdMyCommand.Parameters.Clear()
                    For Each p As ArrayParam In commandParameters
                        sLog += " ," + p.ParamName.Replace("@", "@var") + "=" + p.ParamValue.ToString
                        cmdMyCommand.Parameters.Add(New MySqlParameter(p.ParamName.Replace("@", "@var"), p.ParamValue))
                    Next
                    Dim accessoDB As New MySqlDataAdapter()
                    accessoDB.SelectCommand = cmdMyCommand
                    accessoDB.Fill(myDataSet, "myDataSet")
                Catch ex As Exception
                    Log.Debug("AnagraficaDLL::getDBObject::GetDataSet::si è verificato il seguente errore::" + sLog, ex)
                    myDataSet = Nothing
                Finally
                    cmdMyCommand.Dispose()
                End Try
            Else
                Dim cmdMyCommand As New SqlCommand
                Try
                    cmdMyCommand = myUtility.InizializzaCmdSQL(m_Connection)
                    cmdMyCommand.CommandType = CommandType.StoredProcedure
                    cmdMyCommand.CommandText = sSQL
                    sLog = sSQL
                    cmdMyCommand.Parameters.Clear()
                    For Each p As ArrayParam In commandParameters
                        sLog += " ," + p.ParamName.Replace("@", "@var") + "=" + p.ParamValue.ToString
                        cmdMyCommand.Parameters.Add(New SqlParameter(p.ParamName, p.ParamValue))
                    Next
                    Dim accessoDB As New SqlDataAdapter()
                    accessoDB.SelectCommand = cmdMyCommand
                    accessoDB.Fill(myDataSet, "myDataSet")
                Catch ex As Exception
                    Log.Debug("AnagraficaDLL::getDBObject::GetDataSet::si è verificato il seguente errore::" + sLog, ex)
                    myDataSet = Nothing
                Finally
                    cmdMyCommand.Connection.Dispose()
                    cmdMyCommand.Dispose()
                End Try
            End If
            Return myDataSet
        End Function
        Public Function ExecuteNonQuery(ByVal sSQL As String, ByVal ParamArray commandParameters() As ArrayParam) As Boolean
            Try
                If (_TypeDB = Costanti.DBType_MySQL) Then
                    Dim cmdMyCommand As New MySqlCommand
                    Try
                        cmdMyCommand = myUtility.InizializzaCmd(_TypeDB, m_Connection)
                        cmdMyCommand.Parameters.Clear()
                        For Each p As ArrayParam In commandParameters
                            cmdMyCommand.Parameters.Add(New MySqlParameter(p.ParamName.Replace("@", "@var"), p.ParamValue))
                        Next
                        cmdMyCommand.CommandType = CommandType.StoredProcedure
                        cmdMyCommand.CommandText = sSQL
                        cmdMyCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        Log.Debug("AnagraficaDLL::getDBObject::ExecuteNonQuery::si è verificato il seguente errore::", ex)
                        Return False
                    Finally
                        cmdMyCommand.Dispose()
                    End Try
                Else
                    Dim cmdMyCommand As New SqlCommand
                    Try
                        cmdMyCommand = myUtility.InizializzaCmd(_TypeDB, m_Connection)
                        cmdMyCommand.Parameters.Clear()
                        For Each p As ArrayParam In commandParameters
                            cmdMyCommand.Parameters.Add(New SqlParameter(p.ParamName, p.ParamValue))
                        Next
                        cmdMyCommand.CommandType = CommandType.StoredProcedure
                        cmdMyCommand.CommandText = sSQL
                        cmdMyCommand.ExecuteNonQuery()
                    Catch ex As Exception
                        Log.Debug("AnagraficaDLL::getDBObject::ExecuteNonQuery::si è verificato il seguente errore::", ex)
                        Return False
                    Finally
                        cmdMyCommand.Connection.Dispose()
                        cmdMyCommand.Dispose()
                    End Try
                End If
                Return True
            Catch ex As Exception
                Log.Debug("AnagraficaDLL::getDBObject::ExecuteNonQuery::si è verificato il seguente errore::", ex)
                Return False
            End Try
        End Function
#End Region
#Region "OLD SOLO SQL"
        Protected Function GetSQLConnection() As SqlConnection

            Dim ret_conn As SqlConnection

            If m_Connection = String.Empty Then
                ret_conn = New SqlConnection(ConfigurationManager.AppSettings("connectString"))
            Else
                ret_conn = New SqlConnection(m_Connection)
            End If

            ret_conn.Open()
            GetSQLConnection = ret_conn

        End Function
        Protected Sub CloseSQLConnection(ByVal conn As SqlConnection)
            conn.Close()
            conn = Nothing
        End Sub
        Public Function GetSQLDataReader(ByVal strSQL As String) As SqlDataReader

            Dim cn As SqlConnection = GetSQLConnection()
            Dim rdr As SqlDataReader

            Dim cmd As New SqlCommand(strSQL, cn)
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Dispose()

            Return rdr

        End Function
        Public Function RunSQLActionQueryIdentiy(ByVal strSQL As String) As Integer

            Dim cn As SqlConnection = GetSQLConnection()
            Dim cmd As New SqlCommand(strSQL, cn)
            Dim IDValue As Integer
            Try
                IDValue = cmd.ExecuteScalar()
                cmd.Dispose()
            Finally
                CloseSQLConnection(cn)
            End Try
            Return IDValue
        End Function
        Public Sub RunSQLActionQuery(ByVal strSQL As String)

            Dim cn As SqlConnection = GetSQLConnection()
            Dim cmd As New SqlCommand(strSQL, cn)

            Try
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Finally
                CloseSQLConnection(cn)
            End Try

        End Sub
        Public Sub RunSQLSP(ByVal strSP As String, ByVal ParamArray commandParameters() As SqlParameter)

            Dim cn As SqlConnection = GetSQLConnection()

            Try

                Dim cmd As New SqlCommand(strSP, cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim p As SqlParameter
                For Each p In commandParameters
                    p = cmd.Parameters.Add(p)
                    p.Direction = ParameterDirection.Input
                Next


                cmd.ExecuteNonQuery()

                cmd.Dispose()

            Finally
                CloseSQLConnection(cn)
            End Try
        End Sub
        Public Overloads Function RunSQLSPReturnRS(ByVal strSP As String, ByVal ParamArray commandParameters() As SqlParameter) As SqlDataReader

            Dim cn As SqlConnection = GetSQLConnection()
            Dim rdr As SqlDataReader

            Dim cmd As New SqlCommand(strSP, cn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim p As SqlParameter
            For Each p In commandParameters
                p = cmd.Parameters.Add(p)
                p.Direction = ParameterDirection.Input
            Next

            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Dispose()

            Return rdr

        End Function
        Public Function RunSQLSPReturnInteger(ByVal strSP As String, ByVal ParamArray commandParameters() As SqlParameter) As Integer

            Dim cn As SqlConnection = GetSQLConnection()
            Dim retVal As Integer

            Try

                Dim cmd As New SqlCommand(strSP, cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim p As SqlParameter
                For Each p In commandParameters
                    p = cmd.Parameters.Add(p)
                    p.Direction = ParameterDirection.Input
                Next

                p = cmd.Parameters.Add(New SqlParameter("@RetVal", SqlDbType.Int))
                p.Direction = ParameterDirection.Output

                cmd.ExecuteNonQuery()
                retVal = cmd.Parameters("@RetVal").Value
                cmd.Dispose()

            Finally
                CloseSQLConnection(cn)
            End Try

            Return retVal

        End Function
        Public Function RunSQLSPReturnRowCount(ByVal strSP As String, ByVal ParamArray commandParameters() As SqlParameter) As Integer

            Dim cn As SqlConnection = GetSQLConnection()
            Dim retVal As Integer

            Try

                Dim cmd As New SqlCommand(strSP, cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim p As SqlParameter
                For Each p In commandParameters
                    p = cmd.Parameters.Add(p)
                    p.Direction = ParameterDirection.Input
                Next

                p = cmd.Parameters.Add("@RowCount", SqlDbType.Int)
                p.Direction = ParameterDirection.ReturnValue



                cmd.ExecuteNonQuery()
                retVal = cmd.Parameters("@RowCount").Value
                cmd.Dispose()

            Finally
                CloseSQLConnection(cn)
            End Try

            Return retVal

        End Function
        Public Function RunSQLSPReturnDataSet(ByVal strSP As String, ByVal DataTableName As String, ByVal ParamArray commandParameters() As SqlParameter) As DataSet

            Dim cn As SqlConnection = GetSQLConnection()

            Dim ds As New DataSet()

            Dim da As New SqlDataAdapter(strSP, cn)
            da.SelectCommand.CommandType = CommandType.StoredProcedure

            Dim p As SqlParameter
            For Each p In commandParameters
                da.SelectCommand.Parameters.Add(p)
                p.Direction = ParameterDirection.Input
            Next

            da.Fill(ds, DataTableName)

            CloseSQLConnection(cn)
            da.Dispose()

            Return ds

        End Function
        Public Function RunSQLReturnDataSet(ByVal strSql As String) As DataSet

            Dim cn As SqlConnection = GetSQLConnection()

            Dim ds As New DataSet()

            Dim da As New SqlDataAdapter(strSql, cn)
            da.SelectCommand.CommandType = CommandType.Text

            da.Fill(ds)

            CloseSQLConnection(cn)
            da.Dispose()

            Return ds

        End Function
        Public Function RunSQLReturnDataAdapter(ByVal strSql As String) As SqlDataAdapter

            Dim cn As SqlConnection = GetSQLConnection()

            Dim da As New SqlDataAdapter(strSql, cn)
            da.SelectCommand.CommandType = CommandType.Text
            CloseSQLConnection(cn)

            Return da

        End Function

        Public Function GetSQLConnectionGrid() As SqlConnection

            Dim ret_conn As SqlConnection

            If m_Connection = String.Empty Then
                ret_conn = New SqlConnection(ConfigurationManager.AppSettings("connectString"))
            Else
                ret_conn = New SqlConnection(m_Connection)
            End If

            ret_conn.Open()
            GetSQLConnectionGrid = ret_conn

        End Function

        'Public Function GetSQLNewId(ByRef strNomeTabella As String) As Long

        '    'ANTONELLO
        '    'funzione che estrae il nuovo ID
        '    Dim strSql As String
        '    Dim sqlTrans As SqlTransaction
        '    Dim lngMaxId As Long
        '    Dim oComm As SqlCommand
        '    Dim ret_conn As SqlConnection
        '    ret_conn = New SqlConnection(ConfigurationManager.AppSettings("connectString"))

        '    ret_conn.Open()
        '    sqlTrans = ret_conn.BeginTransaction(IsolationLevel.Serializable)
        '    Try

        '        strSql = "SELECT MAXID FROM CONTATORI  WHERE NOME_TABELLA ='" & strNomeTabella & "'"
        '        oComm = New SqlCommand(strSql, ret_conn, sqlTrans)
        '        Dim dr As SqlDataReader = oComm.ExecuteReader
        '        If dr.Read Then
        '            lngMaxId = dr.Item("MAXID")
        '            lngMaxId = lngMaxId + _const.VALUE_INCREMENT
        '        End If
        '        dr.Close()
        '        strSql = "UPDATE CONTATORI SET MAXID=" & lngMaxId & " WHERE NOME_TABELLA ='" & strNomeTabella & "'"
        '        oComm = New SqlCommand(strSql, ret_conn, sqlTrans)
        '        oComm.ExecuteNonQuery()
        '        sqlTrans.Commit()
        '    Catch ex As Exception
        '        sqlTrans.Rollback()
        '        Throw
        '    Finally
        '        oComm.Dispose()
        '        ret_conn.Close()
        '    End Try

        '    GetSQLNewId = lngMaxId

        'End Function


        Public Function RunSQLSPReturnToGrid(ByVal strSP As String,
            ByRef oConn As SqlConnection,
            ByRef oComm As SqlCommand,
            ByVal ParamArray commandParameters() As SqlParameter) As Integer
            '///Utilizzata per popolare una griglia da una storedprocedure
            '///Deve tornare un oggettocommand,e un  oggetto connection

            Dim cn As SqlConnection = GetSQLConnection()
            Dim retVal As Integer

            oConn = cn
            Dim cmd As New SqlCommand(strSP, cn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim p As SqlParameter
            For Each p In commandParameters
                p = cmd.Parameters.Add(p)
                p.Direction = ParameterDirection.Input
            Next

            p = cmd.Parameters.Add("@RowCount", SqlDbType.Int)
            p.Direction = ParameterDirection.ReturnValue



            cmd.ExecuteNonQuery()
            oComm = cmd
            retVal = cmd.Parameters("@RowCount").Value

            Return retVal
        End Function
#End Region
    End Class

    Public Class ArrayParam
        Public ParamName As String
        Public ParamValue As Object

        Public Sub New(_name As String, _value As Object)
            ParamName = _name
            ParamValue = _value
        End Sub
    End Class
End Namespace
