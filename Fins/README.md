# Osd.Omron.Finsクラス
OMRON FINS Message Communication  
windows11 DotNetFramework4.7.2

### 定義
名前空間: Osd.Omron  
アッセンブリ: fins.dll

OMRONのネットワークで使われている共通のメッセージサービスのコマンドをEthernet経由で送信し接続先機器の状態の読出しや制御を行います
### 例1
次の例はPLCのEthernetポートに接続してユニット情報を読み出します  
PCのIPアドレスは"192.168.0.10" FINSアドレスは"0.10.0"、PLCのIPアドレスは"192.168.0.20" FINSアドレスは"0.20.0"としてUDP接続します
```CS
public void ReadUnitData()
{
    Osd.Omron.Fins fins = new Osd.Omron.Fins();
    fins.ClientFinsAddress = new byte[3] { 0, 10, 0 };
    fins.ServerFinsAddress = new byte[3] { 0, 20, 0 };
    fins.Connect("192.168.0.20");

    byte[] res = fins.ReadUnitData();
    Console.WriteLine(BitConverter.ToString(res));
}
```
### 注釈
クラスのインスタンスfinsを作成して、送受信用のFINSアドレスのプロパティを設定した後でConnectメソッドを呼び出して接続します  
その後にReadUnitDataメソッドでユニット情報の読出しを行っています

# コンストラクター
<table>
  <tr>
    <td width=200 style="border-right:none;"><a href=#fins>Fins()</a></td>
    <td width=500 style="border-left:none;">Finsクラスの新しいインスタンスを初期化します </td>
  </tr>
</table>

# プロパティ
<table>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsmessagelog>MessageLog</a></td>
    <td width=500 style="border-left:none;">通信ログを取得します</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsclientfinsaddress>ClientFinsAddress</a></td>
    <td width=500 style="border-left:none;">送信元FINSアドレスの設定と取得をします</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsserverfinsaddress>ServerFinsAddress</a></td>
    <td width=500 style="border-left:none;">送信先FINSアドレスの設定と取得をします</td>
  </tr>
</table>

# メソッド
<table>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsconnectメソッド>Connect(string, bool)</a></td>
    <td width=500 style="border-left:none;">接続先IPアドレスを指定してUDPまたはTCPで接続</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finscloseメソッド>Close()</a></td>
    <td width=500 style="border-left:none;">接続を終了</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finssendcommandメソッド>SendCommand(byte[])</a></td>
    <td width=500 style="border-left:none;">接続しているホストにFINSコマンドを送信</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsmemoffsetメソッド>MemOffset(string, short)</a></td>
    <td width=500 style="border-left:none;">メモリのアドレス表記をFINSコマンドのメモリ指定方法に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsreadメソッド>read(string, int)</a></td>
    <td width=500 style="border-left:none;">メモリエリアの読出し (0101)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finswriteメソッド>write(string, byte[])</a></td>
    <td width=500 style="border-left:none;">メモリエリアの書込み (0102)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsfillメソッド>fill(string, int, byte[])</a></td>
    <td width=500 style="border-left:none;">メモリエリアの一括書込み (0103)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsmultireadメソッド>MultiRead(string)</a></td>
    <td width=500 style="border-left:none;">メモリエリアの複合読出し (0104)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsrunメソッド>run(byte)</a></td>
    <td width=500 style="border-left:none;">運転モード変更 (0401)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsstopメソッド>stop()</a></td>
    <td width=500 style="border-left:none;">運転停止 (0402)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsreadunitdataメソッド>ReadUnitData()</a></td>
    <td width=500 style="border-left:none;">CPUユニット情報の読出し (0501)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsreadunitstatusメソッド>ReadUnitStatus()</a></td>
    <td width=500 style="border-left:none;">CPUユニットステータスの読出し (0601)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsreadcycletimeメソッド>ReadCycleTime()</a></td>
    <td width=500 style="border-left:none;">サイクルタイム読出し (0620)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finsclockメソッド>Clock()</a></td>
    <td width=500 style="border-left:none;">時間情報読出し (0701)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finssetclockメソッド>SetClock()</a></td>
    <td width=500 style="border-left:none;">時間情報書込み (0702)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finserrorclearメソッド>ErrorClear(byte[])</a></td>
    <td width=500 style="border-left:none;">異常解除 (2101)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finserrorlogreadメソッド>ErrorLogRead(int, int)</a></td>
    <td width=500 style="border-left:none;">異常履歴読出し (2102)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finserrorlogclearメソッド>ErrorLogClear()</a></td>
    <td width=500 style="border-left:none;">異常履歴クリア (2103)</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finswordtobinメソッド>WordToBin(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列を2進数の0または1で表す文字列型に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstobitarrayメソッド>toBitArray(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をBitArray型に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstoboolarrayメソッド>toBoolArray(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をBool型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstoint16メソッド>toInt16(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をInt16型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstoint32メソッド>toInt32(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をInt32型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstoint64メソッド>toInt64(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をInt64型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstouint16メソッド>toUInt16(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をUInt16型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstouint32メソッド>toUInt32(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をUInt32型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstouint64メソッド>toUInt64(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をUInt64型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstofloatメソッド>toFloat(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をFloat型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstodoubleメソッド>toDouble(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をDouble型の配列に変換</td>
  </tr>
  <tr>
    <td width=200 style="border-right:none;"><a href=#finstostringメソッド>toString(byte[])</a></td>
    <td width=500 style="border-left:none;">バイト配列をString型に変換</td>
  </tr>
</table>

# コンストラクター

## Fins()

### 定義
名前空間: Osd.Omron  
アセンブリ: fins.dllの説明  

Finsの新しいインスタンスを初期化します
```cs
public Fins();
```

### 例
```cs
Osd.Omron.Fins fins = new Osd.Omron.Fins();
```

# プロパティ
## Fins.MessageLog
### 定義

通信ログを取得します
```cs
public string MessageLog{ get; };
```

### 例
DM0の値を読出したときのコマンドとレスポンスレスポンスを取得する例です
```cs
public staic viod ReadMemory()
{
	Osd.Omron.Fins fins = new Osd.Omron.Fins();
	fins.ServerFinsAddress = new byte[3] {0, 1, 0};
	fins.ClientFinsAddress = new byte[3] {0, 10, 0}
	fins.Connect("192.168.0.1");
	byte[] res = fins.read("D0", 1);
	string text = fins.MessageLog();
}
```

## Fins.ClientFinsAddress
### 定義
送信元FINSアドレスの設定と取得をします
```CS
public byte[] ClientFinsAddress
```
### 例
```CS
fins.ClientFinsAddress = new byte[3] {0, 10, 0}
```
### 注釈
FINSアドレスはbyte[3]の内容は以下の通りです  
ClientFinsAddress[0] : ネットワークアドレス  
ClientFinsAddress[1] : ノードアドレス  
ClientFinsAddress[2] : 号機アドレス  

## ServerFinsAddress
### 定義
送信先FINSアドレスの設定と取得をします
```CS
public byte[] ServerFinsAddress
```
### 例
```CS
fins.ServerFinsAddress = new byte[3] {0, 1, 0};
```
### 注釈
FINSアドレスはbyte[3]の内容は以下の通りです  
ServerFinsAddress[0] : ネットワークアドレス  
ServerFinsAddress[1] : ノードアドレス  
ServerFinsAddress[2] : 号機アドレス  

# メソッド

## Fins.Connectメソッド
### 定義
指定したIPアドレスと接続します
```CS
public byte[] Connect(string TargetIP, bool TcpConnect = false);
```
### パラメータ
` Target ` string  
接続先アドレス

` TcpConnect ` bool  
trueでTCP接続、falseでUDP接続 （省略時はUDP接続）

### 戻り値
byte[]  

送信元と送信先のFINSノードアドレスを格納したbyte配列  
byte[0]:送信先ノード番号  
byte[1]:送信元ノード番号  

### 例
```CS
public void FinsTcpConnect()
{
	Osd.Omron.Fins fins = new Osd.Omron.Fins();
	fins.ServerFinsAddress = new byte[3] {0, 1, 0};
	fins.ClientFinsAddress = new byte[3] {0, 10, 0}
	byte[] FinsNode = fins.Connect("192.168.0.1", true);
}
```

### 注釈
UDP接続の場合は、UdpClientのインスタンスを作成し指定したIPv4アドレスとポート番号9600番を接続先として設定します。  
TCP接続の場合は、TcpClientのインスタンスを作成し指定したIPv4アドレスとポート番号9600番へ非同期接続し、接続先に「FINS ノードアドレス情報送信コマンド」を送信しノードアドレスを取得します。 以後は取得したノードアドレスを使ってコマンドを作成します

## Fins.Closeメソッド
### 定義
接続を終了します
```CS
public void Close();
```

### 例
すでにConnectしているfinsの接続を終了する例です
```CS
fins.Close();
```

### 注釈
UDP接続の場合は、UdpClientのリソースを解放します  
TCP接続の場合は、TcpClientを閉じるように要求しインスタンスを破棄済みとしてマークします

## Fins.SendCommandメソッド
### 定義
FINSコマンドを送信してレスポンスを受信します
```CS
public byte[] SendCommand(byte[] command)();
```
### 戻り値
byte[]  

レスポンスが格納されたbyte配列

### 例
IPアドレス（192.168.0.20）のPLCにUDP接続してFINSコマンドを送信する例です
```CS
public void FinsSend()
{
	Osd.Omron.Fins fins = new Osd.Omron.Fins();
	fins.Connect("192.168.0.20", "0.20.0", "0.10.0");

	byte[] cmd = new byte[12];
	cmd[0] = 0x80;
	cmd[1] = 0x00;
	cmd[2] = 0x02;
	cmd[3] = 0;     // 送信先FINSアドレス
	cmd[4] = 20;
	cmd[5] = 0;
	cmd[6] = 0;     // 送信元FINSアドレス
	cmd[7] = 10;
	cmd[8] = 0;
	cmd[9] = 1;     // SID
	cmd[10] = 0x05; // CPU情報読出し0501
	cmd[11] = 0x01;

	byte[] res = fins.SendCommand(cmd);
	Console.WriteLine(BitConverter.ToString(res));
}
```

## Fins.MemOffsetメソッド
### 定義
メモリのアドレス表記をFINSコマンドのメモリ指定方法に変換します
```CS
public byte[] MemOffset(string memstring, short offset);
```
### パラメータ
` memstring ` string  
メモリを表す文字列

| メモリ種別 | 表記方法 |
| --- | --- |
|DM|D100|
|EM0|E0_100|
|WR|W100|
|HR|H100|
|CIO|100|

※ ビットは未対応

` offset ` short  
オフセット位置

### 戻り値
byte[]  

FINSコマンドで使用するI/Oメモリの指定に従ったbyte配列  
byte[0]:メモリタイプ  
byte[1]:アドレス（上位）  
byte[2]:アドレス（下位）  
byte[3]:ビット位置 （0x00固定）  

### 例
DM100からオフセット+1のアドレス(D101)を変換する例
```CS
byte[] mem = fins.MemOffset("D100", 1);
Console.WriteLine(BitConverter.ToString(mem));
// mem = 82-00-65-00
```

### 注釈
アドレスはチャネル（ワード）のみ指定可能です  
アドレスとI/Oメモリ種別の対応は以下の通りです

|アドレス|I/Oメモリ種別|
|---|---|
|DM|0x82|
|EM0-F|0xA0-0xAF|
|EM10-18|0x60-0x68|
|WR|0xB1|
|HR|0xB2|
|CIO|0xB0|

## Fins.readメソッド
### 定義
メモリエリアの読出し (0101)
```CS
public byte[] read(string memadrstr, int readsize);
```
### パラメータ
` memadrstr ` string  
メモリを表す文字列

` readsize ` int  
読み出すサイズ

### 戻り値
byte[]  

### 例
DM0から3CH読み出す例
```CS
byte[] res = fins.read("D0", 3);
Console.WriteLine(BitConverter.ToString(res));
// res = 00-01-00-02-00-03
```

### 注釈
D0からD2までの3チャネルを読み出してバイト配列に格納します  
バイト配列を数値などに変換するにはtoInt16()などの変換命令で変換します

## Fins.writeメソッド
### 定義
メモリエリアの書込み (0102)
```CS
public void write(string memadrstr, byte[] data);
```
### パラメータ
` memadrstr ` string  
メモリを表す文字列

` data ` byte[]  
書込みデータ

### 例
D1000からD1999までに連番を書き込む例
```CS
byte[] writedata = new byte[2000];
for (int cnt = 0; cnt < 1000; cnt++)
{
    byte[] data = BitConverter.GetBytes((short)cnt).Reverse().ToArray();
    Array.Copy(data, 0, writedata, cnt * 2, 2);
}
fins.write("D1000", writedata);
```

## Fins.fillメソッド
### 定義
メモリエリアの一括書込み (0103)
```CS
public void fill(string memadrstr, int size, byte[] data);
```
### パラメータ
` memadrstr ` string  
メモリを表す文字列

` data ` byte[]  
書込みデータ （2バイト）

### 例
DM2000からDM2099に数値100を書き込む例
```CS
int data = 100;
byte[] filldata = BitConverter.GetBytes((short)data).Reverse().ToArray();
fins.fill("D2000", 100, filldata);
```

## Fins.MultiReadメソッド
### 定義
メモリエリアの複合読出し (0104)
```CS
public byte[] MultiRead(string memaddresses);
```
### パラメータ
` memaddresses ` string  
メモリを表す文字列（複数 カンマ区切り）

### 例
DM0とDM10とDM50の値を読み出す例
```CS
byte[] res = fins.MultiRead("D0,D10,D50");
```

## Fins.runメソッド
### 定義
運転モード変更 (0401)
```CS
public void run(byte Mode);
```
### パラメータ
` Mode ` byte  
モード

### 例
モニタモードへ変更する例
```CS
fins.run(0x02);
```
### 注釈
モードの種類  
0x02 ：モニタモード  
0x04 ：運転モード  

## Fins.stopメソッド
### 定義
運転停止 (0402)
```CS
public void stop();
```

### 例
```CS
fins.stop();
```

## Fins.ReadUnitDataメソッド
### 定義
CPUユニット情報の読出し (0501)
```CS
public byte[] ReadUnitData();
```

### 例
```CS
byte[] res = fins.ReadUnitData();
```

### 注釈
以下の情報を読み出します
- CPUユニットの形式
- CPU高機能ユニットの構成
- CPUユニットの内部システムのバージョン
- リモートI/O情報
- エリア情報
- CPUユニット情報

## Fins.ReadUnitStatusメソッド
### 定義
CPUユニットステータスの読出し (0601)
```CS
public byte[] ReadUnitStatus();
```

### 例
```CS
byte[] res = fins.ReadUnitStatus();
```

### 注釈
以下の情報を読み出します
- 運転状態
- 運転モード
- 運転停止異常情報
- 運転継続異常情報
- メッセージ有無
- 故障コード
- 異常メッセージ

## Fins.ReadCycleTimeメソッド
### 定義
サイクルタイム読出し (0620)
```CS
public byte[] ReadCycleTime();
```

### 例
```CS
byte[] res = fins.ReadCycleTime();
```

### 注釈
以下の情報を読み出します
- 平均サイクルタイム
- サイクルタイム最大値
- サイクルタイム最小値

## Fins.Clockメソッド
### 定義
時間情報読出し (0701)
```CS
public byte[] Clock();
```

### 例
```CS
byte[] res = fins.Clock();
```

## Fins.SetClockメソッド
### 定義
時間情報書込み (0702)
```CS
public void SetClock();
```

### 例
```CS
fins.SetClock();
```

### 注釈
PCの時間をPLCに書き込みます


## Fins.ErrorClearメソッド
### 定義
異常解除 (2101)
```CS
public void ErrorClear(byte[] code);
```

### パラメータ
` code ` byte[]  
故障コード

### 例
全ての異常を解除する例
```CS
fins.ErrorClear(new byte[2] { 0xFF, 0xFF });
```

### 注釈
code = FFFF: 発生している全ての異常を解除します

## Fins.ErrorLogReadメソッド
### 定義
異常履歴読出し (2102)
```CS
public byte[] ErrorLogRead(int startIndex, int count);
```

### パラメータ
` startIndex ` int  
読出し開始レコードNo

` count ` int  
読出しレコード数

### 例
全ての異常を読み出す例
```CS
byte[] res = fins.ErrorLogRead(0, 20);
```

### 注釈
以下の情報を読み出します
- レコード最大数
- 格納数
- 読出レコード数
- 異常履歴データ １～２０

## Fins.ErrorLogClearメソッド
### 定義
異常履歴クリア (2103)
```CS
public void ErrorLogClear();
```

### 例
```CS
fins.ErrorLogClear();
```

## Fins.WordToBinメソッド
### 定義
バイト配列を2進数の0または1で表す文字列型に変換
```CS
public string WordToBin(byte[] data);
```

### 例
DM0から2CH分の値を読み出して文字列へ格納します
```CS
res = fins.read("D0", 2);
string WordToBin = fins.WordToBin(res);
Console.WriteLine(WordToBin);
//out 00010010001101000101011001111000
```

## Fins.toBitArrayメソッド
### 定義
バイト配列をBitArray型に変換
```CS
public BitArray toBitArray(byte[] data);
```

### 例
DM0から2CH分の値を読み出してBitArray型変数bitsへ格納します
```CS
res = fins.read("D0", 2);
BitArray bits = fins.toBitArray(res);
StringBuilder sb = new StringBuilder();
for (int i = bits.Length; i > 0; i--)
{
    char c = bits[i - 1] ? '1' : '0';
    sb.Append(c);
}
Console.WriteLine(sb.ToString());
//out 00010010001101000101011001111000
```

## Fins.toBoolArrayメソッド
### 定義
バイト配列をbool型の配列に変換
```CS
public bool[] toBoolArray(byte[] data);
```

### 例
DM0から2CH分の値を読み出してbool型の配列へ格納します
```CS
res = fins.read("D0", 2);
bool[] bools = fins.toBoolArray(res);
Console.WriteLine(String.Join(",", bools));
//out False,False,False,True,False,False,True,False, ...
```

## Fins.toInt16メソッド
### 定義
バイト配列をInt16型の配列に変換
```CS
public short[] toInt16(byte[] data);
```

### 例
DM10から10CH分の値を読み出してshort型の配列へ格納します
```CS
res = fins.read("D10", 10);
short[] data16 = fins.toInt16(res);
Console.WriteLine(String.Join(",", data16));
//out 10,11,12,13,14,15,16,17,18,19
```

## Fins.toInt32メソッド
### 定義
バイト配列をInt32型の配列に変換
```CS
public int[] toInt32(byte[] data);
```

### 例
DM20から10CH分の値を読み出してint型の配列へ格納します
```CS
res = fins.read("D20", 10);
int[] data32 = fins.toInt32(res);
Console.WriteLine(String.Join(",", data32));
//out 1376276,1507350,1638424,1769498,1900572
```

## Fins.toInt64メソッド
### 定義
バイト配列をInt64型の配列に変換
```CS
public long[] toInt64(byte[] data);
```

### 例
DM30から8CH分の値を読み出してlong型の配列へ格納します
```CS
res = fins.read("D30", 8);
long[] data64 = fins.toInt64(res);
Console.WriteLine(String.Join(",", data64));
//out 9288811672436766,10414728759410722
```

## Fins.toUInt16メソッド
### 定義
バイト配列をUInt16型の配列に変換
```CS
public ushort[] toUInt16(byte[] data);
```

### 例
DM40から10CH分の値を読み出してushort型の配列へ格納します
```CS
res = fins.read("D40", 10);
ushort[] datau16 = fins.toUInt16(res);
Console.WriteLine(String.Join(",", datau16));
//out 40,41,42,43,44,45,46,47,48,49
```

## Fins.toUInt32メソッド
### 定義
バイト配列をUInt32型の配列に変換
```CS
public uint[] toUInt32(byte[] data);
```

### 例
DM50から10CH分の値を読み出してuint型の配列へ格納します
```CS
res = fins.read("D50", 10);
uint[] datau32 = fins.toUInt32(res);
Console.WriteLine(String.Join(",", datau32));
//out 3342386,3473460,3604534,3735608,3866682
```

## Fins.toUInt64メソッド
### 定義
バイト配列をUInt64型の配列に変換
```CS
public ulong[] toUInt64(byte[] data);
```

### 例
DM60から8CH分の値を読み出してulong型の配列へ格納します
```CS
res = fins.read("D60", 8);
ulong[] datau64 = fins.toUInt64(res);
Console.WriteLine(String.Join(",", datau64));
//out 17733189824741436,18859106911715392
```

## Fins.toFloatメソッド
### 定義
バイト配列をfloat型の配列に変換
```CS
public float[] toFloat(byte[] data);
```

### 例
DM70から10CH分の値を読み出してfloat型の配列へ格納します
```CS
res = fins.read("D70", 10);
float[] dataf = fins.toFloat(res);
Console.WriteLine(String.Join(",", dataf));
//out 6.520418E-39,6.704092E-39,6.887766E-39,7.07144E-39,7.255113E-39
```

## Fins.toDoubleメソッド
### 定義
バイト配列をDouble型の配列に変換
```CS
public double[] toDouble(byte[] data);
```

### 例
DM80から8CH分の値を読み出してdouble型の配列へ格納します
```CS
res = fins.read("D80", 8);
double[] datad = fins.toDouble(res);
Console.WriteLine(String.Join(",", datad));
//out 4.22791874120785E-307,5.11796186559102E-307
```

## Fins.toStringメソッド
### 定義
バイト配列を文字列(UTF8)に変換
```CS
public string toString(byte[] data);
```

### 例
DM90から5CH分の値を読み出して文字列へ格納します
```CS
res = fins.read("D90", 5);
string datastr = fins.toString(res);
Console.WriteLine(datastr);
//out 1234567890
```
