using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class HotFix : MonoBehaviour
{
    private LuaEnv luaEnv;

    private void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(MyLoader);
        luaEnv.DoString("require 'Test'");
    }

    private byte[] MyLoader(ref string filePath)
    {
        string absPath = @"E:\Unity\Lua\" + filePath + ".lua.txt";
        
        string file = File.ReadAllText(absPath);
        return Encoding.UTF8.GetBytes(file);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

}
