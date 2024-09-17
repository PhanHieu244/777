using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
   public void OnPressDown(int i)
   {
      switch (i)
      {
         case 1:
            AddDiamond(10);
             IAPManager.Instance.BuyProductID(IAPKey.PACK1);
            break;
         case 2:
            AddDiamond(30);
            IAPManager.Instance.BuyProductID(IAPKey.PACK2);
            break;
         case 3:
            AddDiamond(50);
            IAPManager.Instance.BuyProductID(IAPKey.PACK3);
            break;
         case 4:
            AddDiamond(100);
            IAPManager.Instance.BuyProductID(IAPKey.PACK4);
            break;
      }
   }

   private void AddDiamond(int a)
   {
      GameManager.instance.setcoin(GameManager.instance.getcoin() + a);
   }

   public void Sub(int i)
   {
      GameDataManager.Instance.playerData.SubDiamond(i);
   }
}
