﻿' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class RatePage
    Inherits Page

    Private Sub Image_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Frame.Navigate(GetType(MainPage))
    End Sub
End Class
