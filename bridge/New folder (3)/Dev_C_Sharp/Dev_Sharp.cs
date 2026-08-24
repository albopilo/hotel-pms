using System.Runtime.InteropServices;
using System.Text;

namespace Dev_C_Sharp;

[Guid("154BD6A6-5AB8-4d7d-A343-E49C4F1826BB")]
public interface Dev_Sharp
{
	[DispId(1)]
	int OpenPort(int portnum, int baud, bool buzzer);

	[DispId(2)]
	int ClosePort(int portnum);

	[DispId(3)]
	int DevBuzzer(byte mill, byte num);

	[DispId(4)]
	int WriteCard(int cardtype, int cardnum, string datetime, string carddata, int datalen, bool Buzzer);

	[DispId(5)]
	int ReadCard(out byte CardType, ref string CardData, bool Buzzer);

	[DispId(6)]
	int ReadCard(ref string CardData, bool Buzzer);

	[DispId(7)]
	int ReadCardS70(StringBuilder lockInfo, StringBuilder recStr, bool Buzzer);

	[DispId(8)]
	int ClearCard(int type, bool Buzzer);

	[DispId(9)]
	int GetRegInfo(StringBuilder regid, StringBuilder regkey);

	[DispId(10)]
	int WriteKey(string regkey);

	[DispId(11)]
	int ChkReg(string regid, string regkey, bool chkid);

	[DispId(12)]
	void GetDevParms(byte[] ver, byte[] initpwd, ref int saler, ref int hotelid);

	[DispId(13)]
	int GetVersion(int len, ref string ver);
}
