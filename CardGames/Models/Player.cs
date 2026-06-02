using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace CardGames.Models
{

//・名前を持つ
//・手札を持つ
//・CPUかどうかを持つ
//・勝ち抜け済みかどうかを持つ
//・手札にカードを追加する
//・手札からカードを取り出す
//・手札枚数を返す
    internal class Player
    {
        //プロパティ名：
        //Name
        //役割：
        //プレイヤー名を保持する。
        //例：
        //・プレイヤー
        //・CPU1
        //・CPU2
        //・CPU3



        //プロパティ名：
        //Hand

        //役割：
        //現在の手札を保持する。

        //内容：
        //Card のリスト。

        //注意：
        //外から勝手に手札を書き換えられると危ないため、
        //最終的には直接変更しにくい形にするか検討する。



        //プロパティ名：
        //IsCpu

        //役割：
        //人間プレイヤーかCPUかを判別する。

        //true：
        //CPU

        //false：
        //人間プレイヤー



        //プロパティ名：
        //IsFinished

        //役割：
        //手札がなくなって勝ち抜け済みかどうかを保持する。

        //true：
        //勝ち抜け済み

        //false：
        //まだゲーム中



        //プロパティ名：
        //HandCount

        //役割：
        //現在の手札枚数を返す。

        //用途：
        //GameFormでCPUの裏向きカード枚数を表示するときに使う。




        //メソッド名：
        //AddCard

        //役割：
        //手札にカードを1枚追加する。

        //入力：
        //Card

        //担当しないこと：
        //ペア削除は行わない。
        //カード追加後の勝敗判定も行わない。




        //メソッド名：
        //RemoveCardAt

        //役割：
        //指定した位置のカードを手札から取り出して返す。

        //入力：
        //cardIndex

        //出力：
        //取り出したCard

        //用途：
        //プレイヤーやCPUが相手の手札からカードを引くときに使う。

        //注意：
        //指定された位置が不正な場合の扱いを決める必要がある。




//プロパティ名：
//IsHandEmpty

//役割：
//手札が0枚かどうかを返す。

//用途：
//勝ち抜け判定に使う。

    }
}
