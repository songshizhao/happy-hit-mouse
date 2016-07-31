' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

Imports System.Xml
Imports Windows.Storage
Imports Windows.UI
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class selectpage
    Inherits Page

    'Private _isread As Boolean = False
    'Public Property isread() As Boolean
    '    Get
    '        Return _isread
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _isread = value
    '    End Set
    'End Property


    Dim all_levels As New ObservableCollection(Of level)
    'Dim level_name， total_score， time_limit， image_path， current_score


    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        Dim isexist As Boolean = True

        '获取文件夹权限
        Dim folder As StorageFolder
        folder = ApplicationData.Current.LocalFolder

        If Await folder.TryGetItemAsync("xmlfile") Is Nothing Then
            isexist = False
            Debug.WriteLine("*******************************************************************")
        End If

        'Try
        '    Dim _xml = Await folder.GetFileAsync("xmlfile")
        '    'isexist = False
        'Catch ex As Exception
        '    isexist = False
        'End Try

        If isexist = False Then
            '读取静态xml文件
            Dim xmlfile As StorageFile = Await StorageFile.GetFileFromApplicationUriAsync(New Uri("ms-appx:///levels\XMLFile1.xml"))
            Dim myfile As Stream = Await xmlfile.OpenStreamForReadAsync()
            Dim mydoc As New XmlDocument
            mydoc.Load(myfile)
            myfile.Dispose()
            Dim myfolder As StorageFolder
            myfolder = ApplicationData.Current.LocalFolder
            Dim xml = Await folder.CreateFileAsync("xmlfile", CreationCollisionOption.ReplaceExisting)
            Dim myfile1 As Stream = Await xml.OpenStreamForWriteAsync
            mydoc.Save(myfile1)
            myfile1.Dispose()
        End If

        Dim my_xml = Await folder.GetFileAsync("xmlfile")
        Dim file As Stream = Await my_xml.OpenStreamForReadAsync()
        Dim doc As New XmlDocument
        doc.Load(file)
        file.Dispose()

        For Each el As XmlElement In doc.DocumentElement
            Dim level_name = el.GetAttribute("关卡名")
            Dim total_score = el.GetAttribute("总分")
            Dim time_limit = el.GetAttribute("时间限制")
            Dim image_path = el.GetAttribute("图标状态")
            Dim current_score = el.GetAttribute("当前得分")
            all_levels.Add(New level(level_name, total_score, time_limit, image_path, current_score))
        Next


        gridview1.DataContext = all_levels

        'Dim levels As XDocument = XDocument.Load("levels\XMLFile1.xml")
        'Dim query = From level In levels.Descendants("level")
        '            Select level_name = level.Attribute("关卡名").Value,
        '                     total_score = level.Attribute("总分").Value，
        '                     time_limit = level.Attribute("时间限制").Value，
        '                     image_path = level.Attribute("图标状态").Value，
        '                     current_score = level.Attribute("当前得分").Value
        'For Each level In query
        '    all_levels.Add(New level(level.level_name, level.total_score, level.time_limit, level.image_path, level.current_score))
        'Next


    End Sub


    '点击后退键，回到主菜单~
    Private Sub Image_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles backbutton.Tapped
        Frame.Navigate(GetType(MainPage))
    End Sub

    Private Sub selectItem_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Dim level_select = sender.tag

        Select Case level_select
            Case "精灵鼠来袭!"
                Frame.Navigate(GetType(GamePage1))
            Case "精灵鼠来袭2!"
                Frame.Navigate(GetType(GamePage2))
            Case "海盗出击"
                Frame.Navigate(GetType(GamePage3))
            Case "流氓兔来捣乱"
                Frame.Navigate(GetType(GamePage4))
            Case "鼠王登场"
                Frame.Navigate(GetType(GamePage5))
            Case "我们都饿坏了！！"
                Frame.Navigate(GetType(GamePage6))
            Case "地鼠革命"
                Frame.Navigate(GetType(GamePage7))
            Case "注意，注意，这不是演习！"
                Frame.Navigate(GetType(GamePage8))
            Case "你的粮食就是我的粮食！"
                Frame.Navigate(GetType(GamePage9))
            Case "背水一战，全军出击！"
                Frame.Navigate(GetType(GamePage10))
            Case Else

        End Select






    End Sub
End Class
