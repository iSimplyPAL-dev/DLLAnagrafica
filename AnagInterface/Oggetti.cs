using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace AnagInterface
{
    [Serializable()]
    public class CRUD
    {
        protected int _id;
        protected DateTime _concurrency;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime Concurrency
        {
            get { return _concurrency; }
            set { _concurrency = value; }
        }
    }
    [Serializable()]
    public class DettaglioAnagrafica : CRUD
    {
        public int m_COD_CONTRIBUENTE = -1;
        public int m_ID_DATA_ANAGRAFICA = -1;
        // //**************DATI NASCITA*******************
        public string m_Cognome = "";
        public string m_Nome = "";
        public string m_CodiceFiscale = "";
        public string m_PartitaIva = "";
        public string m_Sesso = "";
        public string m_CodiceComuneNascita = "-1";
        public string m_ComuneNascita = "";
        public string m_ProvinciaNascita = "";
        public string m_DataNascita = "";
        public string m_NazionalitaNascita = "";
        public string m_CodiceComuneResidenza = "-1";
        public string m_ComuneResidenza = "";
        public string m_ProvinciaResidenza = "";
        public string m_CapResidenza = "";
        public string m_CodViaResidenza = "-1";
        public string m_ViaResidenza = "";
        public string m_PosizioneCivicoResidenza = "";
        public string m_CivicoResidenza = "";
        public string m_EsponenteCivicoResidenza = "";
        public string m_ScalaCivicoResidenza = "";
        public string m_InternoCivicoResidenza = "";
        public string m_FrazioneResidenza = "";
        public string m_NazionalitaResidenza = "";
        private List<ObjIndirizziSpedizione> _listSpedizioni = new List<ObjIndirizziSpedizione>();
        public string m_TipoRiferimento = "";
        public string m_DatiRiferimento = "";
        public object m_dsContatti = null;
        public DataSet m_dsTipiContatti = null;
        public string m_DataValiditaInvioMAIL = "";
        public string m_NucleoFamiliare = "";
        public string m_RappresentanteLegale = "";
        public string m_DataMorte = "";
        public string m_Professione = "";
        public string m_Note = "";
        public bool m_DaRicontrollare = false;
        public string m_Data_Inizio_Validita = "";
        public string m_Data_Fine_Validita = "";
        public string m_Data_Ultima_Modifica = "";
        public string m_Operatore = "";
        public string m_Cod_Contribuente_Rapp_Legale = "-1";
        public string m_CodEnte = "";
        public string m_CodIndividuale = "-1";
        public string m_CodFamiglia = "-1";
        public string m_NC_Tributari = "";
        public string m_Data_Ultimo_Agg_Tributi = "";
        public string m_NC_Anagrafica_Res = "";
        public string m_Data_Ultimo_Agg_Anagrafe = "";
        public int COD_CONTRIBUENTE
        {
            get { return m_COD_CONTRIBUENTE; }
            set { m_COD_CONTRIBUENTE = value; }
        }
        public int ID_DATA_ANAGRAFICA
        {
            get { return m_ID_DATA_ANAGRAFICA; }
            set { m_ID_DATA_ANAGRAFICA = value; }
        }
        public string Cognome
        {
            get { return m_Cognome; }
            set { m_Cognome = value; }
        }
        public string RappresentanteLegale
        {
            get { return m_RappresentanteLegale; }
            set { m_RappresentanteLegale = value; }
        }
        public string Nome
        {
            get { return m_Nome; }
            set { m_Nome = value; }
        }
        public string CodiceFiscale
        {
            get { return m_CodiceFiscale; }
            set { m_CodiceFiscale = value; }
        }
        public string PartitaIva
        {
            get { return m_PartitaIva; }
            set { m_PartitaIva = value; }
        }
        public string CodiceComuneNascita
        {
            get { return m_CodiceComuneNascita; }
            set { m_CodiceComuneNascita = value; }
        }
        public string ComuneNascita
        {
            get { return m_ComuneNascita; }
            set { m_ComuneNascita = value; }
        }
        public string ProvinciaNascita
        {
            get { return m_ProvinciaNascita; }
            set { m_ProvinciaNascita = value; }
        }
        public string DataNascita
        {
            get { return m_DataNascita; }
            set { m_DataNascita = value; }
        }
        public string NazionalitaNascita
        {
            get { return m_NazionalitaNascita; }
            set { m_NazionalitaNascita = value; }
        }
        public string Sesso
        {
            get { return m_Sesso; }
            set { m_Sesso = value; }
        }
        public string CodiceComuneResidenza
        {
            get { return m_CodiceComuneResidenza; }
            set { m_CodiceComuneResidenza = value; }
        }
        public string ComuneResidenza
        {
            get { return m_ComuneResidenza; }
            set { m_ComuneResidenza = value; }
        }
        public string ProvinciaResidenza
        {
            get { return m_ProvinciaResidenza; }
            set { m_ProvinciaResidenza = value; }
        }
        public string CapResidenza
        {
            get { return m_CapResidenza; }
            set { m_CapResidenza = value; }
        }
        public string CodViaResidenza
        {
            get { return m_CodViaResidenza; }
            set { m_CodViaResidenza = value; }
        }
        public string ViaResidenza
        {
            get { return m_ViaResidenza; }
            set { m_ViaResidenza = value; }
        }
        public string PosizioneCivicoResidenza
        {
            get { return m_PosizioneCivicoResidenza; }
            set { m_PosizioneCivicoResidenza = value; }
        }
        public string CivicoResidenza
        {
            get { return m_CivicoResidenza; }
            set { m_CivicoResidenza = value; }
        }
        public string EsponenteCivicoResidenza
        {
            get { return m_EsponenteCivicoResidenza; }
            set { m_EsponenteCivicoResidenza = value; }
        }
        public string ScalaCivicoResidenza
        {
            get { return m_ScalaCivicoResidenza; }
            set { m_ScalaCivicoResidenza = value; }
        }
        public string InternoCivicoResidenza
        {
            get { return m_InternoCivicoResidenza; }
            set { m_InternoCivicoResidenza = value; }
        }
        public string FrazioneResidenza
        {
            get { return m_FrazioneResidenza; }
            set { m_FrazioneResidenza = value; }
        }
        public string NazionalitaResidenza
        {
            get { return m_NazionalitaResidenza; }
            set { m_NazionalitaResidenza = value; }
        }
        public string NucleoFamiliare
        {
            get { return m_NucleoFamiliare; }
            set { m_NucleoFamiliare = value; }
        }
        public string DataMorte
        {
            get { return m_DataMorte; }
            set { m_DataMorte = value; }
        }
        public string Professione
        {
            get { return m_Professione; }
            set { m_Professione = value; }
        }
        public string Note
        {
            get { return m_Note; }
            set { m_Note = value; }
        }
        public bool DaRicontrollare
        {
            get { return m_DaRicontrollare; }
            set { m_DaRicontrollare = value; }
        }
        public string DataInizioValidita
        {
            get { return m_Data_Inizio_Validita; }
            set { m_Data_Inizio_Validita = value; }
        }
        public string DataFineValidita
        {
            get { return m_Data_Fine_Validita; }
            set { m_Data_Fine_Validita = value; }
        }
        public string DataUltimaModifica
        {
            get { return m_Data_Ultima_Modifica; }
            set { m_Data_Ultima_Modifica = value; }
        }
        public string Operatore
        {
            get { return m_Operatore; }
            set { m_Operatore = value; }
        }
        public string CodContribuenteRappLegale
        {
            get { return m_Cod_Contribuente_Rapp_Legale; }
            set { m_Cod_Contribuente_Rapp_Legale = value; }
        }
        public string CodEnte
        {
            get { return m_CodEnte; }
            set { m_CodEnte = value; }
        }
        public string CodIndividuale
        {
            get { return m_CodIndividuale; }
            set { m_CodIndividuale = value; }
        }
        public string CodFamiglia
        {
            get { return m_CodFamiglia; }
            set { m_CodFamiglia = value; }
        }
        public string NCTributari
        {
            get { return m_NC_Tributari; }
            set { m_NC_Tributari = value; }
        }
        public string DataUltimoAggTributi
        {
            get { return m_Data_Ultimo_Agg_Tributi; }
            set { m_Data_Ultimo_Agg_Tributi = value; }
        }
        public string NCAnagraficaRes
        {
            get { return m_NC_Anagrafica_Res; }
            set { m_NC_Anagrafica_Res = value; }
        }
        public string DataUltimoAggAnagrafe
        {
            get { return m_Data_Ultimo_Agg_Anagrafe; }
            set { m_Data_Ultimo_Agg_Anagrafe = value; }
        }
        public List<ObjIndirizziSpedizione> ListSpedizioni
        {
            get { return _listSpedizioni; }
            set { _listSpedizioni = value; }
        }
        public string TipoRiferimento
        {
            get { return m_TipoRiferimento; }
            set { m_TipoRiferimento = value; }
        }
        public string DatiRiferimento
        {
            get { return m_DatiRiferimento; }
            set { m_DatiRiferimento = value; }
        }
        public object dsContatti
        {
            get { return m_dsContatti; }
            set { m_dsContatti = value; }
        }
        public DataSet dsTipiContatti
        {
            get { return m_dsTipiContatti; }
            set { m_dsTipiContatti = value; }
        }
        public string DataValiditaInvioMAIL
        {
            get { return m_DataValiditaInvioMAIL; }
            set { m_DataValiditaInvioMAIL = value; }
        }
    }
    [Serializable()]
    public class ObjIndirizziSpedizione
    {
        private int m_ID_DATA_SPEDIZIONE = -1;
        private string m_COD_TRIBUTO = "";
        private string m_DESCR_TRIBUTO = "";
        private string m_COGNOME_INVIO = "";
        private string m_NOME_INVIO = "";
        private string m_COD_COMUNE_RCP = "-1";
        private string m_COMUNE_RCP = "";
        private string m_LOC_RCP = "";
        private string m_PROVINCIA_RCP = "";
        private string m_CAP_RCP = "";
        private string m_COD_VIA_RCP = "-1";
        private string m_VIA_RCP = "";
        private string m_POSIZIONE_CIV_RCP = "";
        private string m_CIVICO_RCP = "";
        private string m_ESPONENTE_CIVICO_RCP = "";
        private string m_SCALA_CIVICO_RCP = "";
        private string m_INTERNO_CIVICO_RCP = "";
        private string m_FRAZIONE_RCP = "";
        private string m_DATA_INIZIO_VALIDITA_SPED = "";
        private string m_DATA_FINE_VALIDITA_SPED = "";
        private string m_DATA_ULTIMA_MODIFICA_SPED = "";
        private string m_OPERATORE_SPEDIZIONE = "";
        public int ID_DATA_SPEDIZIONE
        {
            get { return m_ID_DATA_SPEDIZIONE; }
            set { m_ID_DATA_SPEDIZIONE = value; }
        }
        public string CodTributo
        {
            get { return m_COD_TRIBUTO; }
            set { m_COD_TRIBUTO = value; }
        }
        public string DescrTributo
        {
            get { return m_DESCR_TRIBUTO; }
            set { m_DESCR_TRIBUTO = value; }
        }
        public string CognomeInvio
        {
            get { return m_COGNOME_INVIO; }
            set { m_COGNOME_INVIO = value; }
        }
        public string NomeInvio
        {
            get { return m_NOME_INVIO; }
            set { m_NOME_INVIO = value; }
        }
        public string CodComuneRCP
        {
            get { return m_COD_COMUNE_RCP; }
            set { m_COD_COMUNE_RCP = value; }
        }
        public string ComuneRCP
        {
            get { return m_COMUNE_RCP; }
            set { m_COMUNE_RCP = value; }
        }
        public string LocRCP
        {
            get { return m_LOC_RCP; }
            set { m_LOC_RCP = value; }
        }
        public string ProvinciaRCP
        {
            get { return m_PROVINCIA_RCP; }
            set { m_PROVINCIA_RCP = value; }
        }
        public string CapRCP
        {
            get { return m_CAP_RCP; }
            set { m_CAP_RCP = value; }
        }
        public string CodViaRCP
        {
            get { return m_COD_VIA_RCP; }
            set { m_COD_VIA_RCP = value; }
        }
        public string ViaRCP
        {
            get { return m_VIA_RCP; }
            set { m_VIA_RCP = value; }
        }
        public string PosizioneCivicoRCP
        {
            get { return m_POSIZIONE_CIV_RCP; }
            set { m_POSIZIONE_CIV_RCP = value; }
        }
        public string CivicoRCP
        {
            get { return m_CIVICO_RCP; }
            set { m_CIVICO_RCP = value; }
        }
        public string EsponenteCivicoRCP
        {
            get { return m_ESPONENTE_CIVICO_RCP; }
            set { m_ESPONENTE_CIVICO_RCP = value; }
        }
        public string ScalaCivicoRCP
        {
            get { return m_SCALA_CIVICO_RCP; }
            set { m_SCALA_CIVICO_RCP = value; }
        }
        public string InternoCivicoRCP
        {
            get { return m_INTERNO_CIVICO_RCP; }
            set { m_INTERNO_CIVICO_RCP = value; }
        }
        public string FrazioneRCP
        {
            get { return m_FRAZIONE_RCP; }
            set { m_FRAZIONE_RCP = value; }
        }
        public string DataInizioValiditaSpedizione
        {
            get { return m_DATA_INIZIO_VALIDITA_SPED; }
            set { m_DATA_INIZIO_VALIDITA_SPED = value; }
        }
        public string DataFineValiditaSpedizione
        {
            get { return m_DATA_FINE_VALIDITA_SPED; }
            set { m_DATA_FINE_VALIDITA_SPED = value; }
        }
        public string DataUltimaModificaSpedizione
        {
            get { return m_DATA_ULTIMA_MODIFICA_SPED; }
            set { m_DATA_ULTIMA_MODIFICA_SPED = value; }
        }
        public string OperatoreSpedizione
        {
            get { return m_OPERATORE_SPEDIZIONE; }
            set { m_OPERATORE_SPEDIZIONE = value; }
        }
    }
    [Serializable()]
    public class DettaglioAnagraficaReturn
    {
        private string m_COD_CONTRIBUENTE = "";
        private string m_NOMINATIVO = "";
        private string m_CODICEFISCALE = "";
        private string m_IDENTE = "";
        public string COD_CONTRIBUENTE
        {
            get { return m_COD_CONTRIBUENTE; }
            set { m_COD_CONTRIBUENTE = value; }
        }
        public string NOMINATIVO
        {
            get { return m_NOMINATIVO; }
            set { m_NOMINATIVO = value; }
        }
        public string CODICEFISCALE
        {
            get { return m_CODICEFISCALE; }
            set { m_CODICEFISCALE = value; }
        }
        public string COD_ENTE
        {
            get { return m_IDENTE; }
            set { m_IDENTE = value; }
        }
    }
}
