using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zigurous.Tweening;

public class CoreBarrier : PulsingMaterial
{
   protected override void Delay()
   {
        Shrink();
        base.Delay();
   }

   private void Shrink()
   {
       this.transform.TweenScale(this.transform.localScale * 0.025f, 0.15f).OnComplete(Grow);
   }

   private void Grow()
   {
       this.transform.TweenScale(this.transform.localScale * 40.0f, 0.15f).SetDelay(2.5f);
   }
}
