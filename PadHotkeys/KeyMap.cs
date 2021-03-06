﻿using System;
using System.Collections.Generic;
using WindowsInput.Native;

namespace PadHotkeys
{
    public class KeyCodes
    {
        public static Dictionary<String, VirtualKeyCode> Map = new Dictionary<String, VirtualKeyCode>() {
            { "lbutton", VirtualKeyCode.LBUTTON },
            { "rbutton", VirtualKeyCode.RBUTTON },
            { "cancel", VirtualKeyCode.CANCEL },
            { "mbutton", VirtualKeyCode.MBUTTON },
            { "xbutton1", VirtualKeyCode.XBUTTON1 },
            { "xbutton2", VirtualKeyCode.XBUTTON2 },
            { "back", VirtualKeyCode.BACK },
            { "tab", VirtualKeyCode.TAB },
            { "clear", VirtualKeyCode.CLEAR },
            { "return", VirtualKeyCode.RETURN },
            { "shift", VirtualKeyCode.SHIFT },
            { "control", VirtualKeyCode.CONTROL },
            { "alt", VirtualKeyCode.MENU },
            { "pause", VirtualKeyCode.PAUSE },
            { "capital", VirtualKeyCode.CAPITAL },
            { "kana", VirtualKeyCode.KANA },
            { "hangeul", VirtualKeyCode.HANGEUL },
            { "hangul", VirtualKeyCode.HANGUL },
            { "junja", VirtualKeyCode.JUNJA },
            { "final", VirtualKeyCode.FINAL },
            { "kanji", VirtualKeyCode.KANJI },
            { "hanja", VirtualKeyCode.HANJA },
            { "escape", VirtualKeyCode.ESCAPE },
            { "convert", VirtualKeyCode.CONVERT },
            { "nonconvert", VirtualKeyCode.NONCONVERT },
            { "accept", VirtualKeyCode.ACCEPT },
            { "modechange", VirtualKeyCode.MODECHANGE },
            { "space", VirtualKeyCode.SPACE },
            { "pgup", VirtualKeyCode.PRIOR },
            { "pgdn", VirtualKeyCode.NEXT },
            { "end", VirtualKeyCode.END },
            { "home", VirtualKeyCode.HOME },
            { "left", VirtualKeyCode.LEFT },
            { "up", VirtualKeyCode.UP },
            { "right", VirtualKeyCode.RIGHT },
            { "down", VirtualKeyCode.DOWN },
            { "select", VirtualKeyCode.SELECT },
            { "print", VirtualKeyCode.PRINT },
            { "execute", VirtualKeyCode.EXECUTE },
            { "snapshot", VirtualKeyCode.SNAPSHOT },
            { "insert", VirtualKeyCode.INSERT },
            { "delete", VirtualKeyCode.DELETE },
            { "help", VirtualKeyCode.HELP },
            { "0", VirtualKeyCode.VK_0 },
            { "1", VirtualKeyCode.VK_1 },
            { "2", VirtualKeyCode.VK_2 },
            { "3", VirtualKeyCode.VK_3 },
            { "4", VirtualKeyCode.VK_4 },
            { "5", VirtualKeyCode.VK_5 },
            { "6", VirtualKeyCode.VK_6 },
            { "7", VirtualKeyCode.VK_7 },
            { "8", VirtualKeyCode.VK_8 },
            { "9", VirtualKeyCode.VK_9 },
            { "a", VirtualKeyCode.VK_A },
            { "b", VirtualKeyCode.VK_B },
            { "c", VirtualKeyCode.VK_C },
            { "d", VirtualKeyCode.VK_D },
            { "e", VirtualKeyCode.VK_E },
            { "f", VirtualKeyCode.VK_F },
            { "g", VirtualKeyCode.VK_G },
            { "h", VirtualKeyCode.VK_H },
            { "i", VirtualKeyCode.VK_I },
            { "j", VirtualKeyCode.VK_J },
            { "k", VirtualKeyCode.VK_K },
            { "l", VirtualKeyCode.VK_L },
            { "m", VirtualKeyCode.VK_M },
            { "n", VirtualKeyCode.VK_N },
            { "o", VirtualKeyCode.VK_O },
            { "p", VirtualKeyCode.VK_P },
            { "q", VirtualKeyCode.VK_Q },
            { "r", VirtualKeyCode.VK_R },
            { "s", VirtualKeyCode.VK_S },
            { "t", VirtualKeyCode.VK_T },
            { "u", VirtualKeyCode.VK_U },
            { "v", VirtualKeyCode.VK_V },
            { "w", VirtualKeyCode.VK_W },
            { "x", VirtualKeyCode.VK_X },
            { "y", VirtualKeyCode.VK_Y },
            { "z", VirtualKeyCode.VK_Z },
            { "lwin", VirtualKeyCode.LWIN },
            { "rwin", VirtualKeyCode.RWIN },
            { "apps", VirtualKeyCode.APPS },
            { "sleep", VirtualKeyCode.SLEEP },
            { "numpad0", VirtualKeyCode.NUMPAD0 },
            { "numpad1", VirtualKeyCode.NUMPAD1 },
            { "numpad2", VirtualKeyCode.NUMPAD2 },
            { "numpad3", VirtualKeyCode.NUMPAD3 },
            { "numpad4", VirtualKeyCode.NUMPAD4 },
            { "numpad5", VirtualKeyCode.NUMPAD5 },
            { "numpad6", VirtualKeyCode.NUMPAD6 },
            { "numpad7", VirtualKeyCode.NUMPAD7 },
            { "numpad8", VirtualKeyCode.NUMPAD8 },
            { "numpad9", VirtualKeyCode.NUMPAD9 },
            { "multiply", VirtualKeyCode.MULTIPLY },
            { "add", VirtualKeyCode.ADD },
            { "separator", VirtualKeyCode.SEPARATOR },
            { "subtract", VirtualKeyCode.SUBTRACT },
            { "decimal", VirtualKeyCode.DECIMAL },
            { "divide", VirtualKeyCode.DIVIDE },
            { "f1", VirtualKeyCode.F1 },
            { "f2", VirtualKeyCode.F2 },
            { "f3", VirtualKeyCode.F3 },
            { "f4", VirtualKeyCode.F4 },
            { "f5", VirtualKeyCode.F5 },
            { "f6", VirtualKeyCode.F6 },
            { "f7", VirtualKeyCode.F7 },
            { "f8", VirtualKeyCode.F8 },
            { "f9", VirtualKeyCode.F9 },
            { "f10", VirtualKeyCode.F10 },
            { "f11", VirtualKeyCode.F11 },
            { "f12", VirtualKeyCode.F12 },
            { "f13", VirtualKeyCode.F13 },
            { "f14", VirtualKeyCode.F14 },
            { "f15", VirtualKeyCode.F15 },
            { "f16", VirtualKeyCode.F16 },
            { "f17", VirtualKeyCode.F17 },
            { "f18", VirtualKeyCode.F18 },
            { "f19", VirtualKeyCode.F19 },
            { "f20", VirtualKeyCode.F20 },
            { "f21", VirtualKeyCode.F21 },
            { "f22", VirtualKeyCode.F22 },
            { "f23", VirtualKeyCode.F23 },
            { "f24", VirtualKeyCode.F24 },
            { "numlock", VirtualKeyCode.NUMLOCK },
            { "scroll", VirtualKeyCode.SCROLL },
            { "lshift", VirtualKeyCode.LSHIFT },
            { "rshift", VirtualKeyCode.RSHIFT },
            { "lcontrol", VirtualKeyCode.LCONTROL },
            { "rcontrol", VirtualKeyCode.RCONTROL },
            { "lalt", VirtualKeyCode.LMENU },
            { "ralt", VirtualKeyCode.RMENU },
            { "browser_back", VirtualKeyCode.BROWSER_BACK },
            { "browser_forward", VirtualKeyCode.BROWSER_FORWARD },
            { "browser_refresh", VirtualKeyCode.BROWSER_REFRESH },
            { "browser_stop", VirtualKeyCode.BROWSER_STOP },
            { "browser_search", VirtualKeyCode.BROWSER_SEARCH },
            { "browser_favorites", VirtualKeyCode.BROWSER_FAVORITES },
            { "browser_home", VirtualKeyCode.BROWSER_HOME },
            { "volume_mute", VirtualKeyCode.VOLUME_MUTE },
            { "volume_down", VirtualKeyCode.VOLUME_DOWN },
            { "volume_up", VirtualKeyCode.VOLUME_UP },
            { "media_next_track", VirtualKeyCode.MEDIA_NEXT_TRACK },
            { "media_prev_track", VirtualKeyCode.MEDIA_PREV_TRACK },
            { "media_stop", VirtualKeyCode.MEDIA_STOP },
            { "media_play_pause", VirtualKeyCode.MEDIA_PLAY_PAUSE },
            { "launch_mail", VirtualKeyCode.LAUNCH_MAIL },
            { "launch_media_select", VirtualKeyCode.LAUNCH_MEDIA_SELECT },
            { "launch_app1", VirtualKeyCode.LAUNCH_APP1 },
            { "launch_app2", VirtualKeyCode.LAUNCH_APP2 },
            { "oem_1", VirtualKeyCode.OEM_1 },
            { "oem_plus", VirtualKeyCode.OEM_PLUS },
            { "oem_comma", VirtualKeyCode.OEM_COMMA },
            { "oem_minus", VirtualKeyCode.OEM_MINUS },
            { "oem_period", VirtualKeyCode.OEM_PERIOD },
            { "oem_2", VirtualKeyCode.OEM_2 },
            { "oem_3", VirtualKeyCode.OEM_3 },
            { "oem_4", VirtualKeyCode.OEM_4 },
            { "oem_5", VirtualKeyCode.OEM_5 },
            { "oem_6", VirtualKeyCode.OEM_6 },
            { "oem_7", VirtualKeyCode.OEM_7 },
            { "oem_8", VirtualKeyCode.OEM_8 },
            { "oem_102", VirtualKeyCode.OEM_102 },
            { "processkey", VirtualKeyCode.PROCESSKEY },
            { "packet", VirtualKeyCode.PACKET },
            { "attn", VirtualKeyCode.ATTN },
            { "crsel", VirtualKeyCode.CRSEL },
            { "exsel", VirtualKeyCode.EXSEL },
            { "ereof", VirtualKeyCode.EREOF },
            { "play", VirtualKeyCode.PLAY },
            { "zoom", VirtualKeyCode.ZOOM },
            { "noname", VirtualKeyCode.NONAME },
            { "pa1", VirtualKeyCode.PA1 },
            { "oem_clear", VirtualKeyCode.OEM_CLEAR }
        };
    }
}