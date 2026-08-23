namespace Dev_C_Sharp;

internal class ErrorCode
{
	internal const int reg_InvData = -100;

	internal const int reg_IDLenEr = -104;

	internal const int reg_IDDataEr = -105;

	internal const int reg_IDUnUse = -106;

	internal const int reg_KeLenEr = -107;

	internal const int reg_KeDataEr = -108;

	internal const int reg_KeUnUse = -109;

	internal const int reg_KeIllegal = -110;

	internal const int reg_CopyIllegal = -111;

	internal const int reg_UnReg = -112;

	internal const int reg_UnRegistered = -2000;

	internal const int reg_KeUUse = -114;

	internal const int reg_KeTOut = -115;

	internal const int reg_TimeOut = -2001;

	internal const int reg_CNReg = -116;

	internal const int reg_IDCHK = -117;

	internal const int reg_TIllegal = -118;

	internal const int reg_GetIEr = -120;

	internal const int reg_WriIEr = -121;

	internal const int reg_WriILenEr = -122;

	internal const int Err_Dev_Port = -800;

	internal const int Err_Dev_Open = -801;

	internal const int Err_Dev_NULLPort = -802;

	internal const int Err_Dev_Port_STILL = -803;

	internal const int Err_COM_SETSIZE = -811;

	internal const int Err_COM_SETTIME = -812;

	internal const int Err_COM_SETSTATE = -813;

	internal const int Err_COM_GETSTATE = -814;

	internal const int Err_Dev_Send = -831;

	internal const int Err_Dev_SYNC = -832;

	internal const int Err_Dev_W_BYTE = -833;

	internal const int Err_Dev_R_BYTE = -834;

	internal const int Err_Dev_Rece = -835;

	internal const int Err_Dev_Rece_Head = -836;

	internal const int Err_Dev_Rece_End = -837;

	internal const int Err_Dev_Rece_Len = -838;

	internal const int Err_Dev_Rece_Chk = -839;

	internal const int Err_Dev_Rece_Addr = -840;

	internal const int Err_Dev_Rece_NULL = -841;

	internal const int Err_Dev_Rece_Oper = -842;

	internal const int Err_Dev_W_TIMEOUT = -843;

	internal const int Err_Dev_R_TIMEOUT = -844;

	internal const int Err_Dev_NOANSWER = -845;

	internal const int Err_Dev_CMD = -846;

	internal const int Err_HID_Send = -601;

	internal const int Err_HID_RECE = -602;

	internal const int Err_Data = -1001;

	internal const int Err_Write_DEF_CHK = -1101;

	internal const int Err_Write_DEF_ERR = -1102;

	internal const int Err_Write_DEF_MAK = -1103;

	internal const int Err_Read_CONFIG = -1201;

	internal const int Err_Read_DEF = -1202;

	internal const int Err_Read_CHK = -1203;

	internal const int CommandErrorState = -1;

	internal const int CommandOkState = 0;

	internal const int CommandOk = 128;

	internal const int CommandError = 129;

	internal const int CommandTimeOut = 130;

	internal const int CardError = 131;

	internal const int CommandRevErr = 132;

	internal const int CommandParmErr = 133;

	internal const int UnKnowErr = 135;

	internal const int CommandNotExist = 143;
}
