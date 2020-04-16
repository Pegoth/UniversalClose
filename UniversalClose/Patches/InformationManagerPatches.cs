using HarmonyLib;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    internal class InformationManagerPatches
    {
        [HarmonyPatch(typeof(InformationManager), "ShowInquiry")]
        internal class ShowInquiryPatch
        {
            public static void Prefix(ref InquiryData data)
            {
                var origAff = data.AffirmativeAction;
                var origNeg = data.NegativeAction;

                data = new InquiryData(
                    data.TitleText,
                    data.Text,
                    data.IsAffirmativeOptionShown,
                    data.IsNegativeOptionShown,
                    data.AffirmativeText,
                    data.NegativeText,
                    () =>
                    {
                        try
                        {
                            origAff?.Invoke();
                        }
                        finally
                        {
                            UniversalCloseModule.IsInquaryVisible = true;
                            DebugLogger.Print("Called: InquiryData.AffirmativeAction");
                        }
                    },
                    () =>
                    {
                        try
                        {
                            origNeg?.Invoke();
                        }
                        finally
                        {
                            UniversalCloseModule.IsInquaryVisible = false;
                            DebugLogger.Print("Called: InquiryData.NegativeAction");
                        }
                    },
                    data.SoundEventPath
                );
            }

            public static void Postfix()
            {
                UniversalCloseModule.IsInquaryVisible = true;
                DebugLogger.Print("Called: InformationManager.ShowInquiry");
            }
        }

        [HarmonyPatch(typeof(InformationManager), "HideInquiry")]
        internal class HideInquiryPatch
        {
            public static void Postfix()
            {
                UniversalCloseModule.IsInquaryVisible = false;
                DebugLogger.Print("Called: InformationManager.HideInquiry");
            }
        }

        [HarmonyPatch(typeof(InformationManager), "ClearAllMessages")]
        internal class ClearAllMessagesPatch
        {
            public static void Postfix()
            {
                UniversalCloseModule.IsInquaryVisible = false;
                DebugLogger.Print("Called: InformationManager.ClearAllMessages");
            }
        }
    }
}