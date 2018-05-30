'https://www.cnblogs.com/luyichuan/archive/2012/02/22/2363338.html
'https://www.cnblogs.com/armyfai/p/3890476.html

Public Class Form1
    Public i As Integer = 0
    Public tryTime As Integer = 0
    Public IsReady As Boolean = False
    Dim time As Integer = 0
    Public username As String
    Public password As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim data As String
        Dim f2 As System.IO.StreamWriter = New System.IO.StreamWriter("settings.txt", True, System.Text.Encoding.UTF8)
        f2.Close()
        data = System.IO.File.ReadAllText("settings.txt", System.Text.Encoding.UTF8)
        If data = "" Then
            Dim f1 As System.IO.StreamWriter = New System.IO.StreamWriter("settings.txt", False, System.Text.Encoding.UTF8)
            f1.Write("[username] = " & Chr(34) & Chr(34) & vbCrLf & "[password] = " & Chr(34) & Chr(34))
            f1.Close()
            MessageBox.Show("请修改软件根目录下的settings.txt，再重启。" & vbCrLf & "注意，你的账号密码的储存方式是不安全的" & vbCrLf & "made by banic")
            Me.Close()
            Exit Sub
        Else
            username = LookUp(data, "[username]")
            password = LookUp(data, "[password]")

        End If
        ' 登陆网站
        Me.WebBrowser1.Navigate("https://net.zju.edu.cn/")
    End Sub

    ' 网站加载完成后的事件
    ' 注意这里仅在第一次加载完成后进行处理
    ' 可以试试去掉case操作，看看结果如何
    Private Sub wBrowser_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        IsReady = True
        i += 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim log As HtmlWindow = Me.MainBrowser.Document   ' 登陆窗口所在帧
        '' 填写用户名密码，并触发确定按钮, 使用name属性
        'log.Document.All("username").SetAttribute("value", username)
        'log.Document.All("passwd").SetAttribute("value", passwd)
        'log.Document.All("submit1").InvokeMember("click")


        'Select Case time
        '    Case 0
        '        '' wd为baidu中输入搜索内容的标志,注意搜索按钮触发的方法
        '        'Me.WebBrowser1.Document.All("wd").SetAttribute("value", "你想搜索的文字")
        '        'Me.WebBrowser1.Document.Forms(0).InvokeMember("submit")
        '        'time += 1

        '        Dim log = Me.WebBrowser1   ' 登陆窗口所在帧
        '        ' 填写用户名密码，并触发确定按钮, 使用name属性
        '        log.Document.All("username").SetAttribute("value", "username")
        '        log.Document.All("password").SetAttribute("value", "password")
        '        'log.Document.All("Submit").InvokeMember("Click")
        '        WebBrowser1.Document.GetElementById("button").InvokeMember("click")
        '        'Me.WebBrowser1.Document.Forms(0).InvokeMember("Submit")
        '        time += 1
        'End Select

        If IsReady Then
            '    Select Case time
            '        Case 0
            '' wd为baidu中输入搜索内容的标志,注意搜索按钮触发的方法
            'Me.WebBrowser1.Document.All("wd").SetAttribute("value", "你想搜索的文字")
            'Me.WebBrowser1.Document.Forms(0).InvokeMember("submit")
            'time += 1
            'Dim log = Me.WebBrowser1   ' 登陆窗口所在帧 我觉得没必要
            ' 填写用户名密码，并触发确定按钮, 使用name属性
            WebBrowser1.Document.All("username").SetAttribute("value", username)
            WebBrowser1.Document.All("password").SetAttribute("value", password)
            'log.Document.All("Submit").InvokeMember("Click")
            WebBrowser1.Document.GetElementById("button").InvokeMember("click")
            '        time += 1
            'End Select


            ' 登陆网站
            If i = 0 Then
                'Me.WebBrowser1.Navigate("https://net.zju.edu.cn/")
                Button1.Text = "It has not been loaded yet!"
            Else
                Me.WebBrowser1.Refresh()
            End If
            i += 1
        Else
            tryTime += 1
            Button1.Text = "Not Ready: " & tryTime
        End If
    End Sub

    'https://zhidao.baidu.com/question/274850061.html?qbl=relate_question_2&word=vbnet%20%CB%D1%CB%F7%D6%D8%B8%B4%B3%F6%CF%D6%B5%C4%B5%DA%B6%FE%B8%F6%D7%D6%B7%FB
    Public Function LookUp(ByVal data, ByVal key)
        Dim strStart As Int16 = InStr(data, key)
        data = Mid(data, strStart)
        strStart = InStr(1, data, Chr(34))
        Dim strEnd As Int16 = InStr(strStart + 1, data, Chr(34))
        Return Mid(data, strStart + 1, strEnd - strStart - 1) '第二个数是从strStart开始算起

    End Function
End Class
