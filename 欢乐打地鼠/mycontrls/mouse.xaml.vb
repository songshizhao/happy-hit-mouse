Public NotInheritable Class mouse
    Inherits UserControl
    Dim WithEvents mytimer As New DispatcherTimer
    Dim stay_time As Double


    Public Event HitSucess()
    Public Event HitFailed()

    Dim _LongestTime As Double = 12 'defalt12秒
    Property LongestTime（）
        Set(value)
            _LongestTime = value
        End Set
        Get
            LongestTime = _LongestTime
        End Get
    End Property
    '老鼠图片
    Private mouse_image As Uri
    Public Property mouse_image_source() As Uri
        Get
            Return mouse_image
        End Get
        Set(ByVal value As Uri)
            mouse_image = value
        End Set
    End Property
    '老鼠被击打后图片
    Private mouse_image_hit As Uri
    Public Property mouse_image_source_hit() As Uri
        Get
            Return mouse_image_hit
        End Get
        Set(ByVal value As Uri)
            mouse_image_hit = value
        End Set
    End Property

    '老鼠叫声
    Private mouse_hit_sound As Uri
    Public Property mouse_hit_sound_source() As Uri
        Get
            Return mouse_hit_sound
        End Get
        Set(ByVal value As Uri)
            mouse_hit_sound = value
        End Set
    End Property

    Private _onoff As Boolean = True
    Public Property onoff() As Boolean
        Get
            Return _onoff
        End Get
        Set(ByVal value As Boolean)
            _onoff = value
        End Set
    End Property



    Private Sub mouse_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles mouse_pic.Tapped
        mouse_pic.Source = New BitmapImage(mouse_image_source_hit) 'New BitmapImage(New Uri("ms-appx:///images\mouseh.png"))
        hitsound.Source = mouse_hit_sound
        star.Source = New BitmapImage(New Uri("ms-appx:///images\star1.png"))
        地鼠晃动动画.Stop()
        地鼠打回动画.Begin()


    End Sub



    Private Sub mouse_Loaded(sender As Object, e As RoutedEventArgs)
        mouse_pic.Source = Nothing
        Dim rnd As New Random(Me.Tag + Date.Now.Second.ToString)
        stay_time = rnd.Next(0, _LongestTime)
        mytimer.Interval = New TimeSpan(0, 0, stay_time)
        mytimer.Start()

    End Sub


    ''' <summary>
    ''' 地鼠钻出动画播放完成，等待1秒时间进行晃动
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 地鼠钻出动画_Completed(sender As Object, e As Object)
        地鼠晃动动画.Begin()
    End Sub
    ''' <summary>
    ''' 晃动结束后钻回洞里面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 地鼠晃动动画_Completed(sender As Object, e As Object)
        地鼠钻回动画.Begin()
    End Sub


    ''' <summary>
    ''' 眩晕动画结束后，返回地洞
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 地鼠打回动画_Completed(sender As Object, e As Object)

        RaiseEvent HitSucess()

        star.Source = Nothing
        '针对渲染的变化进行修正
        地鼠钻出.Rotation = 0
        '设置潜伏随机数
        Dim rnd As New Random(Me.Tag)
        stay_time = rnd.Next(0, _LongestTime)
        mytimer.Interval = New TimeSpan(0, 0, stay_time)
        mytimer.Start()
    End Sub

    ''' <summary>
    ''' 地鼠钻回完成后，换回原图片
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub 地鼠钻回动画_Completed(sender As Object, e As Object)
        If onoff Then
            '打击失败
            RaiseEvent HitFailed()

            '针对渲染的变化进行修正
            地鼠钻出.Rotation = 0
            '设置潜伏随机数
            Dim rnd As New Random(Me.Tag)
            stay_time = rnd.Next(0, _LongestTime)
            mytimer.Interval = New TimeSpan(0, 0, stay_time)
            mytimer.Start()
        End If

    End Sub

    Sub stay_time_ended() Handles mytimer.Tick

        地鼠钻出动画.Begin()
        mouse_pic.Source = New BitmapImage(mouse_image_source)
        mytimer.Stop()
    End Sub


End Class
