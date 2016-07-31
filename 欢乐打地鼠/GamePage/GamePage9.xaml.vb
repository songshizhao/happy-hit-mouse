

Imports System.Xml
Imports Windows.Storage
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class GamePage9
    Inherits Page

    Const 孔洞 As Integer = 6
    Const 游戏时间 = 60
    Const 潜伏时间 = 14
    Const 目标分数 = 游戏时间 / (潜伏时间 / 2) * 孔洞 * 100 '每只老鼠100分~~
    Dim limate As Double = 12


    Dim WithEvents ContrlBox As New GameControl(游戏时间)

    Private Sub Page_Loading(sender As Object, e As RoutedEventArgs) Handles MyBase.Loading
        '------------------------------------------进行游戏的初始化控制

        For Each amouse In grid1.Children
            If TypeOf amouse Is mouse Then
                Dim each_mouse As mouse = amouse
                '每个老鼠的潜伏最长时间设为xx秒
                each_mouse.LongestTime = 潜伏时间
                each_mouse.mouse_image_source = New Uri("ms-appx:///images\craze_mouse.png")
                each_mouse.mouse_image_source_hit = New Uri("ms-appx:///images\hit_craze_mouse.png")
                each_mouse.mouse_hit_sound_source = New Uri("ms-appx:///sounds\mouse0.mp3")

                AddHandler each_mouse.HitSucess, AddressOf mouse_HitSucess
                AddHandler each_mouse.HitFailed, AddressOf mouse_HitFailed
            End If

        Next

        '游戏的初始得分设置为0
        ContrlBox.Scores = 0
        ContrlBox.VerticalAlignment = VerticalAlignment.Top
        ContrlBox.HorizontalAlignment = HorizontalAlignment.Left
        ContrlBox.SetValue(Grid.ColumnSpanProperty, 3)
        grid1.Children.Add(ContrlBox)


    End Sub

    '游戏结束
    Private Async Sub GameOver() Handles ContrlBox.TimeArrived
        ContrlBox.onoff = False
        For Each amouse In grid1.Children
            If TypeOf amouse Is mouse Then
                Dim each_mouse As mouse = amouse
                each_mouse.onoff = False
                RemoveHandler each_mouse.HitFailed, AddressOf mouse_HitFailed
                RemoveHandler each_mouse.HitSucess, AddressOf mouse_HitSucess
            End If
        Next

        '------------------

        Dim para(2) As Double
        para(1) = 3
        para(2) = ContrlBox.Scores
        Frame.Navigate(GetType(ScorePage), para)

        '------------------------------------------读写数据
        Dim folder As StorageFolder
        folder = ApplicationData.Current.LocalFolder

        Dim my_xml = Await folder.GetFileAsync("xmlfile")
        Dim file As Stream = Await my_xml.OpenStreamForReadAsync()

        Dim doc As New XmlDocument
        doc.Load(file)
        'file.Dispose()


        For Each el As XmlElement In doc.DocumentElement
            If el.GetAttribute（"关卡名"） = "你的粮食就是我的粮食！" Then
                If ContrlBox.Scores > el.GetAttribute（"总分"） Then
                    ContrlBox.Scores = el.GetAttribute（"总分"）
                End If
                el.SetAttribute("当前得分", ContrlBox.Scores)
            End If
        Next

        Dim xml = Await folder.CreateFileAsync("xmlfile", CreationCollisionOption.ReplaceExisting)
        Dim file1 As Stream = Await xml.OpenStreamForWriteAsync
        doc.Save(file1)
        file1.Dispose()
    End Sub

    Private Sub page_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles MyBase.PointerPressed
        hammerhitsound.Source = New Uri("ms-appx:///sounds\no_hit.mp3")
        Dim click_point As New Point
        click_point = e.GetCurrentPoint(sender).Position
        hammer.Margin = New Thickness(click_point.X - 30, click_point.Y - 130, 0, 0)
        锤子动画.Begin()
    End Sub

    Private Sub mouse_HitSucess()
        ContrlBox.Scores += 100
    End Sub

    Private Sub mouse_HitFailed()
        fail_sound.Source = New Uri("ms-appx:///sounds\mouse4.mp3")
        limate -= 1
        If limate < 0 Then
            ContrlBox.onoff = False
            For Each amouse In grid1.Children
                If TypeOf amouse Is mouse Then
                    Dim each_mouse As mouse = amouse
                    each_mouse.onoff = False
                    RemoveHandler each_mouse.HitFailed, AddressOf mouse_HitFailed
                    RemoveHandler each_mouse.HitSucess, AddressOf mouse_HitSucess
                End If
            Next
            Frame.Navigate(GetType(FailPage))
        End If
    End Sub


    Private Sub back_Tapped(sender As Object, e As TappedRoutedEventArgs)
        ContrlBox.onoff = False
        For Each amouse In grid1.Children
            If TypeOf amouse Is mouse Then
                Dim each_mouse As mouse = amouse
                each_mouse.onoff = False
                RemoveHandler each_mouse.HitFailed, AddressOf mouse_HitFailed
                RemoveHandler each_mouse.HitSucess, AddressOf mouse_HitSucess
            End If
        Next
        Frame.GoBack()
    End Sub


End Class
