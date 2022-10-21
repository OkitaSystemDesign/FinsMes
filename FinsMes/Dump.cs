using System;
using System.Collections.Generic;
using System.Text;

namespace FinsMes
{
    class Dump
    {
        public static string Execute(byte[] byteArray, int ColMax)
        {
            if (byteArray == null)
                return null;

            StringBuilder sbLine = new StringBuilder();
            StringBuilder sbAll = new StringBuilder();
            string hex;
            int pos = 0;
            int col = 0;
            //int colMax = 10;    // 1行に表示したいバイト数

            for (int i = 0; i < byteArray.Length; i++)
            {
                if (col == 0)
                {
                    // 新しい行のセッティング
                    sbLine = new StringBuilder();
                    sbLine.Append(" : ");
                    pos = 0;
                }

                // 16進数表記に変換する
                if (byteArray[i] <= 0x0F)
                    hex = "0" + String.Format("{0:X}", byteArray[i]);
                else
                    hex = String.Format("{0:X}", byteArray[i]);

                // 16進数表記を追加
                sbLine.Insert(pos, hex + " ");
                col++;
                pos += 3;

                if (byteArray[i] >= 0x20 && byteArray[i] < 0x7F)
                {
                    // ASCII 文字の場合は ASCII 文字を追加
                    sbLine.Append(Convert.ToChar(byteArray[i]));
                }
                else
                {
                    //  ASCII 文字以外の場合は "." を追加
                    sbLine.Append(".");
                }

                if (col == ColMax)
                {
                    // 1行に表示したいバイト数に達したら
                    sbLine.Append(Environment.NewLine);
                    sbAll.Append(sbLine.ToString());
                    col = 0;
                }
            }

            if (col != 0)
            {
                while (pos < ColMax * 3)
                {
                    sbLine.Insert(pos, " ");
                    pos++;
                }
                sbAll.Append(sbLine.ToString());
            }

            return sbAll.ToString();
        }
    }
}
