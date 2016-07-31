' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class SettingPage
    Inherits Page

    Private Sub Button_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Frame.Navigate(GetType(MainPage))
    End Sub
End Class
