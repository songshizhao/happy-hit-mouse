' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class GameControl
    Inherits UserControl
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(<CallerMemberName()> Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

#Region "成员"
    Private _Scores As Integer
    Private _MaxTime As Integer
    Private _CurrentTime As Double = 0
    Public WithEvents GameTimer As New DispatcherTimer
    Public Event TimeArrived()
#End Region

#Region "事件"
    ''' <summary>
    ''' 初始化游戏时间控制
    ''' </summary>
    ''' <param name="GameTime">游戏计时时间</param>
    Sub New(GameTime)

        InitializeComponent() ' 此调用是设计器所必需的。
        GameTimer.Interval = New TimeSpan(0, 0, 0, 0, GameTime)
        GameTimer.Start()

        MyPanel.DataContext = Me
        Me.MaxTime = GameTime

    End Sub

    Private Sub TimeTicked() Handles GameTimer.Tick
        _CurrentTime += 0.1
        Me.CurrentTime = _CurrentTime
        If onoff = False Then
            GameTimer.Stop()
        End If
        If (_CurrentTime >= MaxTime) Then
            '结束计时器
            GameTimer.Stop()
            '触发游戏时间结束事件
            RaiseEvent TimeArrived()
        End If

    End Sub
#End Region

#Region "属性"

    ''' <summary>
    ''' 得分
    ''' </summary>
    ''' <returns></returns>
    Public Property Scores() As Integer
        Get
            Return _Scores
        End Get
        Set(ByVal value As Integer)
            _Scores = value
            NotifyPropertyChanged()
        End Set
    End Property


    ''' <summary>
    ''' 最长时间
    ''' </summary>
    ''' <returns></returns>
    Public Property MaxTime() As Integer
        Get
            Return _MaxTime
        End Get
        Set(ByVal value As Integer)
            _MaxTime = value
            NotifyPropertyChanged()
        End Set
    End Property



    Public Property CurrentTime() As Double
        Get
            Return _CurrentTime
        End Get
        Set(ByVal value As Double)
            _CurrentTime = value
            NotifyPropertyChanged()
        End Set
    End Property

    Private _onoff As Boolean = True
    Public Property onoff() As Boolean
        Get
            Return _onoff
        End Get
        Set(ByVal value As Boolean)
            _onoff = value
            NotifyPropertyChanged()
        End Set
    End Property




#End Region






End Class
