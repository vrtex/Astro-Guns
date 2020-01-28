#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("vxDstSBJ/583N6giZXCr7V9a3pEiSZGMkZpX3rdr091wbl6RobrhuSm7d9xMDuzouFRoixt2DCegL+zW5s5xXXM2W7IgFP+Zvx2HHu50UwyAMrGSgL22uZo2+DZHvbGxsbWwsyN+xVJ0PP02WDzRhJ8t83KFRl/DbfLM5AUDMkkMMTHXQq5rjsySDxnqU5ymFFk4/uh4ieMblUsGp3uQU6gIyV0XLqssxFhF14epToEwT6lKE17UiPgIVOkcx9ZNdKuI/0G0Xi2cJOM41DqHHEwdzhfvC7VdfsOrVkWGadClT6A4dCzX7XHURRI1rmpb7/wRHhxQHyniCS86FqWtScHOwfsysb+wgDKxurIysbGwAVca0ACWasviJq7NgLkhb7KzsbCx");
        private static int[] order = new int[] { 9,5,13,8,9,13,6,10,8,11,11,11,12,13,14 };
        private static int key = 176;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
