Public Class level
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="a">关卡名称</param>
    ''' <param name="b">关卡总分</param>
    ''' <param name="c">时间限制</param>
    ''' <param name="d">关卡状态图片</param>
    ''' <param name="e">当前得分</param>
    Public Sub New(a, b, c, d, e)
        _level_name = a
        _total_score = b
        _time_limate = c
        _image_path = d
        _current_score = e
    End Sub
    Sub New()

    End Sub
    Private _level_name As String
    Property level_name
        Get
            level_name = _level_name
        End Get
        Set(value)
            _level_name = value
        End Set
    End Property



    Private _total_score As String
    Property total_score
        Get
            total_score = _total_score
        End Get
        Set(value)
            _total_score = value
        End Set
    End Property


    Private _time_limate As String
    Property time_limate
        Get
            time_limate = _time_limate
        End Get
        Set(value)
            _time_limate = value
        End Set
    End Property


    Private _image_path As String
    Property image_path
        Get
            image_path = _image_path
        End Get
        Set(value)
            _image_path = value
        End Set
    End Property

    Private _current_score As String
    Property current_score
        Get
            current_score = _current_score
        End Get
        Set(value)
            _current_score = value
        End Set
    End Property





End Class
