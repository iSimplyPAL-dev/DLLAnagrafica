Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports log4net
'Imports RIBESFrameWork

Namespace DLL
    Public Class GetListaIndirizziStorici
        Public oConn As SqlConnection
        Public oComm As SqlCommand
        Public lngRecordCount As Integer
    End Class

    Public Class Storico
        Inherits AnagInterface.DettaglioAnagrafica
        Private Log As ILog = LogManager.GetLogger(GetType(Storico))
        Dim DBAccess As New getDBobject()
        '//Costanti
        Dim Costant As New Costanti()
        '//Utility 
        Dim Utility As New AnagUtility()
        Dim ModDate As New GestDate()
        Dim DataSetContatti As New DataSetContatti()
        Dim cmdMyCommand As SqlCommand
        Dim drDetailsAnagrafica As SqlDataReader
        '===============================================================================
        'WorkFlow
        '===============================================================================
        'Dim m_oSession As New RIBESFrameWork.Session()
        Dim m_IDSottoAttivita As String
        '===============================================================================
        'WorkFlow
        '===============================================================================
        '===============================================================================
        'WorkFlow
        '===============================================================================
        'Public Sub New(ByVal oSession As RIBESFrameWork.Session, ByVal IDSottoAttivita As String)
        '    m_oSession = oSession
        '    m_IDSottoAttivita = IDSottoAttivita
        'End Sub
        Public Sub New()
        End Sub
        '===============================================================================
        'WorkFlow
        '===============================================================================
#Region "RIBESFRAMEWORK"
        'Public Function GetStoricoAnagrafica(ByVal COD_CONTRIBUENTE As Long, ByVal COD_TRIBUTO As String, ByVal IDDATAANAGRAFICA As Long) As DettaglioAnagrafica
        '    Dim strSql As String
        '    Dim DettaglioAnagrafica As New DettaglioAnagrafica
        '    '===============================================================================
        '    'WorkFlow
        '    '===============================================================================
        '    Dim objDBAccess As New RIBESFrameWork.DBManager
        '    Try
        '        objDBAccess = m_oSession.GetPrivateDBManager(m_IDSottoAttivita)
        '        '===============================================================================
        '        'WorkFlow
        '        '===============================================================================

        '        'Gestione Contatti
        '        '********************************************************************************************************************
        '        strSql = ""
        '        '*** 20140515 - invio mail ***
        '        'strSql = "SELECT * FROM CONTATTI" & vbCrLf
        '        'strSql = strSql & "INNER JOIN ANAGRAFICA ON CONTATTI.COD_CONTRIBUENTE = ANAGRAFICA.COD_CONTRIBUENTE AND CONTATTI.IDDATAANAGRAFICA = ANAGRAFICA.IDDATAANAGRAFICA" & vbCrLf
        '        'strSql = strSql & "WHERE ANAGRAFICA.COD_CONTRIBUENTE =" & COD_CONTRIBUENTE & vbCrLf
        '        'strSql = strSql & "AND" & vbCrLf
        '        'strSql = strSql & "(ANAGRAFICA.DATA_FINE_VALIDITA IS NULL OR ANAGRAFICA.DATA_FINE_VALIDITA='')" & vbCrLf
        '        'strSql = strSql & "ORDER BY CONTATTI.TIPORIFERIMENTO"
        '        strSql = "SELECT *"
        '        strSql += " FROM V_GETCONTATTI"
        '        strSql += " WHERE COD_CONTRIBUENTE=" & COD_CONTRIBUENTE
        '        '*** ***
        '        '===============================================================================
        '        'WorkFlow
        '        '===============================================================================
        '        DataSetContatti.daContattiPersona = objDBAccess.GetPrivateDataAdapter(strSql)
        '        '===============================================================================
        '        'WorkFlow
        '        '===============================================================================

        '        Dim ds As dsContatti = DataSetContatti.DataSetCompleto
        '        'Caricamento dei contatti associati alla persona se esistenti
        '        DettaglioAnagrafica.dsContatti = ds

        '        strSql = ""
        '        strSql = "SELECT * FROM ANAGRAFICA" & vbCrLf
        '        strSql = strSql & "WHERE" & vbCrLf
        '        strSql = strSql & "ANAGRAFICA.COD_CONTRIBUENTE = " & COD_CONTRIBUENTE & vbCrLf
        '        strSql = strSql & "AND" & vbCrLf
        '        strSql = strSql & "ANAGRAFICA.IDDATAANAGRAFICA=" & IDDATAANAGRAFICA & vbCrLf

        '        '===============================================================================
        '        'WorkFlow
        '        '===============================================================================
        '        objDBAccess = m_oSession.GetPrivateDBManager(m_IDSottoAttivita)
        '        Dim drDetailsAnagrafica As SqlDataReader = objDBAccess.GetPrivateDataReader(strSql)
        '        '===============================================================================
        '        'WorkFlow
        '        '===============================================================================

        '        If drDetailsAnagrafica.Read Then
        '            'Dati Nascita
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.COD_CONTRIBUENTE = Utility.GetParametro(drDetailsAnagrafica("COD_CONTRIBUENTE"))
        '            DettaglioAnagrafica.ID_DATA_ANAGRAFICA = Utility.CIdFromDB(drDetailsAnagrafica("IDDATAANAGRAFICA"))
        '            DettaglioAnagrafica.Cognome = Utility.GetParametro(drDetailsAnagrafica("COGNOME_DENOMINAZIONE"))
        '            DettaglioAnagrafica.Nome = Utility.GetParametro(drDetailsAnagrafica("NOME"))
        '            DettaglioAnagrafica.CodiceFiscale = Utility.GetParametro(drDetailsAnagrafica("COD_FISCALE"))
        '            DettaglioAnagrafica.PartitaIva = Utility.GetParametro(drDetailsAnagrafica("PARTITA_IVA"))
        '            DettaglioAnagrafica.CodiceComuneNascita = Utility.GetParametro(drDetailsAnagrafica("COD_COMUNE_NASCITA"))
        '            DettaglioAnagrafica.ComuneNascita = Utility.GetParametro(drDetailsAnagrafica("COMUNE_NASCITA"))
        '            DettaglioAnagrafica.ProvinciaNascita = Utility.GetParametro(drDetailsAnagrafica("PROV_NASCITA"))
        '            DettaglioAnagrafica.DataNascita = ModDate.GiraDataFromDB(Utility.GetParametro(drDetailsAnagrafica("DATA_NASCITA")))
        '            DettaglioAnagrafica.NazionalitaNascita = Utility.GetParametro(drDetailsAnagrafica("NAZIONALITA_NASCITA"))
        '            DettaglioAnagrafica.Sesso = Utility.GetParametro(drDetailsAnagrafica("SESSO"))
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            'Dati Residenza
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.CodiceComuneResidenza = Utility.GetParametro(drDetailsAnagrafica("COD_COMUNE_RES"))
        '            DettaglioAnagrafica.ComuneResidenza = Utility.GetParametro(drDetailsAnagrafica("COMUNE_RES"))
        '            DettaglioAnagrafica.ProvinciaResidenza = Utility.GetParametro(drDetailsAnagrafica("PROVINCIA_RES"))
        '            DettaglioAnagrafica.CapResidenza = Utility.GetParametro(drDetailsAnagrafica("CAP_RES"))
        '            DettaglioAnagrafica.CodViaResidenza = Utility.GetParametro(drDetailsAnagrafica("COD_VIA_RES"))
        '            DettaglioAnagrafica.ViaResidenza = Utility.GetParametro(drDetailsAnagrafica("VIA_RES"))
        '            DettaglioAnagrafica.PosizioneCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("POSIZIONE_CIVICO_RES"))
        '            DettaglioAnagrafica.CivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("CIVICO_RES"))
        '            DettaglioAnagrafica.EsponenteCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("ESPONENTE_CIVICO_RES"))
        '            DettaglioAnagrafica.ScalaCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("SCALA_CIVICO_RES"))
        '            DettaglioAnagrafica.InternoCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("INTERNO_CIVICO_RES"))
        '            DettaglioAnagrafica.FrazioneResidenza = Utility.GetParametro(drDetailsAnagrafica("FRAZIONE_RES"))
        '            DettaglioAnagrafica.NazionalitaResidenza = Utility.GetParametro(drDetailsAnagrafica("NAZIONALITA_RES"))
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            'Dati generici
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.Professione = Utility.GetParametro(drDetailsAnagrafica("PROFESSIONE"))
        '            DettaglioAnagrafica.Note = Utility.GetParametro(drDetailsAnagrafica("NOTE"))
        '            DettaglioAnagrafica.DaRicontrollare = Utility.cToBool(drDetailsAnagrafica("DA_RICONTROLLARE"))
        '            DettaglioAnagrafica.NucleoFamiliare = Utility.GetParametro(drDetailsAnagrafica("NUCLEO_FAMILIARE"))
        '            DettaglioAnagrafica.CodContribuenteRappLegale = Utility.GetParametro(drDetailsAnagrafica("COD_CONTRIBUENTE_RAPP_LEGALE"))
        '            DettaglioAnagrafica.Operatore = Utility.GetParametro(drDetailsAnagrafica("OPERATORE"))

        '            If IsDBNull(drDetailsAnagrafica("CURRENCY")) Then
        '                DettaglioAnagrafica.Concurrency = CType(Now, Date)
        '            Else
        '                DettaglioAnagrafica.Concurrency = drDetailsAnagrafica("CURRENCY")
        '            End If
        '            '*** 20141027 - visualizzazione tutti indirizzi spedizione ***
        '            'DettaglioAnagrafica.CodTributo = COD_TRIBUTO
        '            '*** ***
        '        End If

        '        drDetailsAnagrafica.Close()
        '        If Len(DettaglioAnagrafica.CodContribuenteRappLegale) > 0 Then
        '            strSql = ""
        '            strSql = "SELECT * FROM ANAGRAFICA" & vbCrLf
        '            strSql = strSql & "INNER JOIN" & vbCrLf
        '            strSql = strSql & "DATA_VALIDITA_ANAGRAFICA ON ANAGRAFICA.COD_CONTRIBUENTE = DATA_VALIDITA_ANAGRAFICA.COD_CONTRIBUENTE" & vbCrLf
        '            strSql = strSql & "AND" & vbCrLf
        '            strSql = strSql & "ANAGRAFICA.IDDATAANAGRAFICA = DATA_VALIDITA_ANAGRAFICA.IDDATAANAGRAFICA" & vbCrLf
        '            strSql = strSql & "WHERE DATA_VALIDITA_ANAGRAFICA.COD_CONTRIBUENTE = " & DettaglioAnagrafica.CodContribuenteRappLegale & vbCrLf
        '            strSql = strSql & "AND" & vbCrLf
        '            strSql = strSql & "(ANAGRAFICA.DATA_FINE_VALIDITA IS NOT NULL OR ANAGRAFICA.DATA_FINE_VALIDITA<>'')"
        '            '===============================================================================
        '            'WorkFlow
        '            '===============================================================================
        '            objDBAccess = m_oSession.GetPrivateDBManager(m_IDSottoAttivita)
        '            drDetailsAnagrafica = objDBAccess.GetPrivateDataReader(strSql)
        '            '===============================================================================
        '            'WorkFlow
        '            '===============================================================================

        '            If drDetailsAnagrafica.Read Then
        '                DettaglioAnagrafica.RappresentanteLegale = Utility.GetParametro(drDetailsAnagrafica("COGNOME_DENOMINAZIONE")) & " " & Utility.GetParametro(drDetailsAnagrafica("NOME"))
        '            End If
        '            drDetailsAnagrafica.Close()
        '        End If
        '    Catch ex As Exception
        '        Log.Debug("GetSoricoanagrafica::si è verificato il seguente errore::", ex)
        '    End Try
        '    Return DettaglioAnagrafica
        'End Function

        'Public Function WF_GetListaINDIRIZZIForRibesDataGridStorico(ByVal COD_TRIBUTO As String, ByVal COD_CONTRIBUENTE As String) As GetListaIndirizziStorici
        '    Dim GetListaIndirizziStorici As New GetListaIndirizziStorici
        '    Dim objConn As New SqlConnection
        '    Dim objCmd As New SqlCommand
        '    '===============================================================================
        '    'WorkFlow
        '    '===============================================================================
        '    Dim objDBAccess As New RIBESFrameWork.DBManager
        '    objDBAccess = m_oSession.GetPrivateDBManager(m_IDSottoAttivita)

        '    '===============================================================================
        '    'WorkFlow
        '    '===============================================================================
        '    ' GetListaIndirizziStorici.lngRecordCount = objDBAccess.GetPrivateRunSPForRibesDataGrid("sp_ListaIndirizziStorico", objConn, objCmd, _
        '    'New SqlParameter("@COD_TRIBUTO", COD_TRIBUTO), _
        '    'New SqlParameter("@COD_CONTRIBUENTE", COD_CONTRIBUENTE))
        '    GetListaIndirizziStorici.oConn = objConn
        '    GetListaIndirizziStorici.oComm = objCmd

        '    Return GetListaIndirizziStorici

        'End Function
#End Region

#Region "NO RIBESFRAMEWORK"
        '*** 20141027 - visualizzazione tutti indirizzi spedizione ***
        'Public Function GetStoricoAnagrafica(ByVal COD_CONTRIBUENTE As Long, ByVal COD_TRIBUTO As String, ByVal IDDATAANAGRAFICA As Long, ByVal StringConnection As String) As DettaglioAnagrafica
        '    Dim strSql As String
        '    Dim DettaglioAnagrafica As New DettaglioAnagrafica

        '    Try
        '        cmdMyCommand = Utility.InizializzaCmd(StringConnection)

        '        'Gestione Contatti
        '        '********************************************************************************************************************
        '        '*** 20140515 - invio mail ***
        '        'strSql = "SELECT * FROM CONTATTI" & vbCrLf
        '        'strSql = strSql & "INNER JOIN ANAGRAFICA ON CONTATTI.COD_CONTRIBUENTE = ANAGRAFICA.COD_CONTRIBUENTE AND CONTATTI.IDDATAANAGRAFICA = ANAGRAFICA.IDDATAANAGRAFICA" & vbCrLf
        '        'strSql = strSql & "WHERE ANAGRAFICA.COD_CONTRIBUENTE =" & COD_CONTRIBUENTE & vbCrLf
        '        'strSql = strSql & "AND" & vbCrLf
        '        'strSql = strSql & "(ANAGRAFICA.DATA_FINE_VALIDITA IS NULL OR ANAGRAFICA.DATA_FINE_VALIDITA='')" & vbCrLf
        '        'strSql = strSql & "ORDER BY CONTATTI.TIPORIFERIMENTO"
        '        strSql = "SELECT *"
        '        strSql += " FROM V_GETCONTATTI"
        '        strSql += " WHERE COD_CONTRIBUENTE=" & COD_CONTRIBUENTE
        '        '*** ***
        '        cmdMyCommand.CommandType = CommandType.Text
        '        cmdMyCommand.CommandText = strSql
        '        Log.Debug("AnagraficaDLL::Storico::GetStoricoAnagrafica::" + strSql)
        '        DataSetContatti.daContattiPersona = New SqlDataAdapter(cmdMyCommand)
        '        Dim ds As dsContatti = DataSetContatti.DataSetCompleto
        '        'Caricamento dei contatti associati alla persona se esistenti
        '        DettaglioAnagrafica.dsContatti = ds

        '        strSql = ""
        '        strSql = "SELECT * FROM ANAGRAFICA" & vbCrLf
        '        strSql = strSql & "WHERE" & vbCrLf
        '        strSql = strSql & "ANAGRAFICA.COD_CONTRIBUENTE = " & COD_CONTRIBUENTE & vbCrLf
        '        strSql = strSql & "AND" & vbCrLf
        '        strSql = strSql & "ANAGRAFICA.IDDATAANAGRAFICA=" & IDDATAANAGRAFICA & vbCrLf
        '        cmdMyCommand.CommandType = CommandType.Text
        '        cmdMyCommand.CommandText = strSql
        '        Log.Debug("AnagraficaDLL::Storico::GetStoricoAnagrafica::" + strSql)
        '        drDetailsAnagrafica = cmdMyCommand.ExecuteReader()

        '        If drDetailsAnagrafica.Read Then
        '            'Dati Nascita
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.COD_CONTRIBUENTE = Utility.GetParametro(drDetailsAnagrafica("COD_CONTRIBUENTE"))
        '            DettaglioAnagrafica.ID_DATA_ANAGRAFICA = Utility.CIdFromDB(drDetailsAnagrafica("IDDATAANAGRAFICA"))
        '            DettaglioAnagrafica.Cognome = Utility.GetParametro(drDetailsAnagrafica("COGNOME_DENOMINAZIONE"))
        '            DettaglioAnagrafica.Nome = Utility.GetParametro(drDetailsAnagrafica("NOME"))
        '            DettaglioAnagrafica.CodiceFiscale = Utility.GetParametro(drDetailsAnagrafica("COD_FISCALE"))
        '            DettaglioAnagrafica.PartitaIva = Utility.GetParametro(drDetailsAnagrafica("PARTITA_IVA"))
        '            DettaglioAnagrafica.CodiceComuneNascita = Utility.GetParametro(drDetailsAnagrafica("COD_COMUNE_NASCITA"))
        '            DettaglioAnagrafica.ComuneNascita = Utility.GetParametro(drDetailsAnagrafica("COMUNE_NASCITA"))
        '            DettaglioAnagrafica.ProvinciaNascita = Utility.GetParametro(drDetailsAnagrafica("PROV_NASCITA"))
        '            DettaglioAnagrafica.DataNascita = ModDate.GiraDataFromDB(Utility.GetParametro(drDetailsAnagrafica("DATA_NASCITA")))
        '            DettaglioAnagrafica.NazionalitaNascita = Utility.GetParametro(drDetailsAnagrafica("NAZIONALITA_NASCITA"))
        '            DettaglioAnagrafica.Sesso = Utility.GetParametro(drDetailsAnagrafica("SESSO"))
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            'Dati Residenza
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.CodiceComuneResidenza = Utility.GetParametro(drDetailsAnagrafica("COD_COMUNE_RES"))
        '            DettaglioAnagrafica.ComuneResidenza = Utility.GetParametro(drDetailsAnagrafica("COMUNE_RES"))
        '            DettaglioAnagrafica.ProvinciaResidenza = Utility.GetParametro(drDetailsAnagrafica("PROVINCIA_RES"))
        '            DettaglioAnagrafica.CapResidenza = Utility.GetParametro(drDetailsAnagrafica("CAP_RES"))
        '            DettaglioAnagrafica.CodViaResidenza = Utility.GetParametro(drDetailsAnagrafica("COD_VIA_RES"))
        '            DettaglioAnagrafica.ViaResidenza = Utility.GetParametro(drDetailsAnagrafica("VIA_RES"))
        '            DettaglioAnagrafica.PosizioneCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("POSIZIONE_CIVICO_RES"))
        '            DettaglioAnagrafica.CivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("CIVICO_RES"))
        '            DettaglioAnagrafica.EsponenteCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("ESPONENTE_CIVICO_RES"))
        '            DettaglioAnagrafica.ScalaCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("SCALA_CIVICO_RES"))
        '            DettaglioAnagrafica.InternoCivicoResidenza = Utility.GetParametro(drDetailsAnagrafica("INTERNO_CIVICO_RES"))
        '            DettaglioAnagrafica.FrazioneResidenza = Utility.GetParametro(drDetailsAnagrafica("FRAZIONE_RES"))
        '            DettaglioAnagrafica.NazionalitaResidenza = Utility.GetParametro(drDetailsAnagrafica("NAZIONALITA_RES"))
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            'Dati generici
        '            '********************************************************************************************************************
        '            '********************************************************************************************************************
        '            DettaglioAnagrafica.Professione = Utility.GetParametro(drDetailsAnagrafica("PROFESSIONE"))
        '            DettaglioAnagrafica.Note = Utility.GetParametro(drDetailsAnagrafica("NOTE"))
        '            DettaglioAnagrafica.DaRicontrollare = Utility.cToBool(drDetailsAnagrafica("DA_RICONTROLLARE"))
        '            DettaglioAnagrafica.NucleoFamiliare = Utility.GetParametro(drDetailsAnagrafica("NUCLEO_FAMILIARE"))
        '            DettaglioAnagrafica.CodContribuenteRappLegale = Utility.GetParametro(drDetailsAnagrafica("COD_CONTRIBUENTE_RAPP_LEGALE"))
        '            DettaglioAnagrafica.Operatore = Utility.GetParametro(drDetailsAnagrafica("OPERATORE"))

        '            If IsDBNull(drDetailsAnagrafica("CURRENCY")) Then
        '                DettaglioAnagrafica.Concurrency = CType(Now, Date)
        '            Else
        '                DettaglioAnagrafica.Concurrency = drDetailsAnagrafica("CURRENCY")
        '            End If
        '            DettaglioAnagrafica.CodTributo = COD_TRIBUTO
        '        End If

        '        drDetailsAnagrafica.Close()
        '        If Len(DettaglioAnagrafica.CodContribuenteRappLegale) > 0 Then
        '            strSql = ""
        '            strSql = "SELECT * FROM ANAGRAFICA" & vbCrLf
        '            strSql = strSql & "INNER JOIN" & vbCrLf
        '            strSql = strSql & "DATA_VALIDITA_ANAGRAFICA ON ANAGRAFICA.COD_CONTRIBUENTE = DATA_VALIDITA_ANAGRAFICA.COD_CONTRIBUENTE" & vbCrLf
        '            strSql = strSql & "AND" & vbCrLf
        '            strSql = strSql & "ANAGRAFICA.IDDATAANAGRAFICA = DATA_VALIDITA_ANAGRAFICA.IDDATAANAGRAFICA" & vbCrLf
        '            strSql = strSql & "WHERE DATA_VALIDITA_ANAGRAFICA.COD_CONTRIBUENTE = " & DettaglioAnagrafica.CodContribuenteRappLegale & vbCrLf
        '            strSql = strSql & "AND" & vbCrLf
        '            strSql = strSql & "(ANAGRAFICA.DATA_FINE_VALIDITA IS NOT NULL OR ANAGRAFICA.DATA_FINE_VALIDITA<>'')"
        '            cmdMyCommand.CommandType = CommandType.Text
        '            cmdMyCommand.CommandText = strSql
        '            Log.Debug("AnagraficaDLL::Storico::GetStoricoAnagrafica::" + strSql)
        '            drDetailsAnagrafica = cmdMyCommand.ExecuteReader()

        '            If drDetailsAnagrafica.Read Then
        '                DettaglioAnagrafica.RappresentanteLegale = Utility.GetParametro(drDetailsAnagrafica("COGNOME_DENOMINAZIONE")) & " " & Utility.GetParametro(drDetailsAnagrafica("NOME"))
        '            End If
        '            drDetailsAnagrafica.Close()
        '            cmdMyCommand.Connection.Close()
        '            cmdMyCommand.Dispose()
        '        End If
        '    Catch ex As Exception
        '        Log.Debug("GetStoricoAnagrafica::si è verificato il seguente errore::", ex)
        '    End Try
        '    Return DettaglioAnagrafica
        'End Function

        Public Function GetStoricoAnagrafica(ByVal COD_CONTRIBUENTE As Long, ByVal IDDATAANAGRAFICA As Long, DBType As String, ByVal StringConnection As String) As AnagInterface.DettaglioAnagrafica
            Try
                Dim FncAnag As New GestioneAnagrafica
                Return FncAnag.GetAnagrafica(COD_CONTRIBUENTE, ID_DATA_ANAGRAFICA, "", DBType, StringConnection, False)
            Catch ex As Exception
                Log.Debug("GetStoricoAnagrafica::si è verificato il seguente errore::", ex)
                Return Nothing
            End Try
        End Function
        '*** ***
#End Region
    End Class
End Namespace