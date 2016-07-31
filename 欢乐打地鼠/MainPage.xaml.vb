'“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

Imports System.Net
Imports Windows.Phone.UI.Input
Imports Windows.UI
Imports Windows.UI.Xaml.Media.Animation
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Dim index As String

    Private Sub MainMenu_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles MainMenu.SelectionChanged
        Dim listbox As ListView = sender
        index = listbox.SelectedItem.tag
        rabit_rotation_out.Begin()
    End Sub

    Private Sub back_Tapped(sender As Object, e As TappedRoutedEventArgs)
        'APPbar中点击了后退键
        If Frame.CanGoBack Then
            Frame.GoBack()
        End If
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        rabit_rotation_in.Begin()
    End Sub

    Private Sub DoubleAnimation_Completed(sender As Object, e As Object)
        Select Case index
            Case “1”
                '点击开始游戏，进入选择关卡页面
                Frame.Navigate(GetType(selectpage))
            Case “2”
                '点击打分和评价，跳转到商店给应用打分
                Frame.Navigate(GetType(RatePage))
            Case “3”
                '点击设置进入设置页面(设想用于调节音量（是否可以震动？）)
                Frame.Navigate(GetType(ScorePage))
            Case “4”

                Frame.Navigate(GetType(AuthorPage))
        End Select


    End Sub

    Private Sub AdControl_AdClick(sender As Object, e As JiuYouAdUniversal.Models.AdClickEventArgs)
        banner_ad.Visibility = Visibility.Collapsed
    End Sub
End Class
