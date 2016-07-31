' “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

Imports Windows.Storage
''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class ScorePage
    Inherits Page
    Const BEST As String = "BEST_SCORE"
    Dim BEST_SCORE As Double
    Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)

        '读取历史数据
        Dim root As ApplicationDataContainer = ApplicationData.Current.LocalSettings
        Dim ReadedScore As Double
        If root.Values.TryGetValue(BEST, ReadedScore) Then
            '读取历史数据，如果存在则读取
            BEST_SCORE = CInt(ReadedScore)
        Else
            '如不存在，新建历史数据
            BEST_SCORE = 0
            '增加数据记录为0
            root.Values(BEST) = 0
        End If

        Dim this_score As Double
        Try
            '获取得分数据,如果存在则读取
            this_score = e.Parameter(2)
        Catch ex As Exception
            '如果不存在，设置为0
            this_score = 0
        End Try

        If this_score > BEST_SCORE Then
            '如果当前得分大于历史数据，那么替换掉历史数据
            root.Values(BEST) = this_score

            History_Best_Score.Text = this_score
        Else
            History_Best_Score.Text = BEST_SCORE
        End If

        '设置页面显示
        CurrentScore.Text = this_score



    End Sub



    Private Sub back_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Frame.Navigate(GetType(selectpage))
    End Sub




End Class
