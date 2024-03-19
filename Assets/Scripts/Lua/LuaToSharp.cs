using UnityEngine;
using XLua;

public class LuaToSharp : MonoBehaviour
{
    LuaEnv luaenv = null;

    [CSharpCallLua]
    public delegate double LuaMax(double a, double b);

    void Start()
    {
        luaenv = new LuaEnv();
        luaenv.DoString("CS.UnityEngine.Debug.Log('hello world')");
        var max = luaenv.Global.GetInPath<LuaMax>("math.max");
        Debug.Log("max:" + max(32, 12));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
