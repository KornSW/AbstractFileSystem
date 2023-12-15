Imports System
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Xml
Imports System.Reflection

Public Module sss

  <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Function ExtractRegions(source As String, regionBeginTag As String, regionEndTag As String, Optional includeTags As Boolean = False, Optional startsInRegion As Boolean = False) As String
    Dim regionContent As New StringBuilder
    source.ExtractRegions(regionBeginTag, regionEndTag, Sub(s) regionContent.Append(s), includeTags, startsInRegion)
    Return regionContent.ToString()
  End Function

  <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Sub ExtractRegions(source As String, regionBeginTag As String, regionEndTag As String, regionCatcher As Action(Of String), Optional includeTags As Boolean = False, Optional startsInRegion As Boolean = False)

    Dim regionCode As New StringBuilder
    Dim sourceUBound As Integer = source.Length - 1
    Dim currentlyInRegion As Boolean = startsInRegion
    Dim currentPosition As Integer = 0
    Dim charsUntilNextSwitch As Integer = -1

    While (currentPosition < sourceUBound)
      If (currentlyInRegion) Then

        charsUntilNextSwitch = source.IndexOf(regionEndTag, currentPosition) - currentPosition

        If (charsUntilNextSwitch < 0) Then
          If (includeTags) Then
            regionCatcher.Invoke(source.Substring(currentPosition, source.Length - currentPosition))
          Else
            regionCatcher.Invoke(source.Substring(currentPosition, source.Length - currentPosition))
          End If
          currentPosition = sourceUBound
        Else
          If (includeTags) Then
            regionCatcher.Invoke(source.Substring(currentPosition, charsUntilNextSwitch + regionEndTag.Length))
          Else
            regionCatcher.Invoke(source.Substring(currentPosition, charsUntilNextSwitch))
          End If
          currentPosition = currentPosition + charsUntilNextSwitch + regionEndTag.Length
          currentlyInRegion = False
        End If

      Else '(Not currentlyInRegion)

        charsUntilNextSwitch = source.IndexOf(regionBeginTag, currentPosition) - currentPosition

        If (charsUntilNextSwitch < 0) Then
          currentPosition = sourceUBound
        Else
          If (includeTags) Then
            currentPosition = currentPosition + charsUntilNextSwitch
          Else
            currentPosition = currentPosition + charsUntilNextSwitch + regionBeginTag.Length
          End If
          currentlyInRegion = True
        End If

      End If
    End While

  End Sub

  <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Function ParseXml(rawString As String) As XmlNodeList
    Dim xDoc = New XmlDocument()
    'we need to wrap the xml in our own root-node because
    'an xmldocument cannot have multiple nodes at root level
    'and we dont know how many root tags the rawString has 
    xDoc.InnerXml = "<XML>" & rawString & "</XML>"
    Return xDoc.FirstChild().ChildNodes()
  End Function

  <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Function ToXmlString(Of TObj)(instance As TObj) As String
    Return instance.ToXmlString(New List(Of Object))
  End Function

  <Extension(), EditorBrowsable(EditorBrowsableState.Advanced)>
  Private Function ToXmlString(Of TObj)(instance As TObj, processedInstances As List(Of Object)) As String
    If (processedInstances.Contains(instance)) Then
      Return String.Empty
    Else

      Dim xDoc = New XmlDocument()
      Dim newElement = Function(name As String) xDoc.CreateElement(name)
      Dim objNode = xDoc.AppendChild(newElement(instance.GetType().Name))

      If (instance.GetType().IsTypeWithoutSubProperties) Then
        objNode.InnerXml = instance.ToString()
      End If

      For Each pInfo As PropertyInfo In instance.GetType.GetProperties(Reflection.BindingFlags.Instance Or BindingFlags.Public)
        If (pInfo.CanRead AndAlso Not pInfo.GetMethod.GetParameters().Any()) Then

          Try
            Dim value As Object = pInfo.GetMethod.Invoke(instance, Nothing)
            If (value Is Nothing) Then
              objNode.AppendChild(newElement(pInfo.Name)).InnerXml = "(NULL)"
            Else
              Dim valueType As Type = pInfo.PropertyType


              If (valueType.IsTypeWithoutSubProperties) Then
                objNode.AppendChild(newElement(pInfo.Name)).InnerXml = value.ToString() '.Replace(Environment.NewLine, "")
              Else


                Select Case True

                  Case valueType.IsArray
                    'If (valueType.GetItemType().IsTypeWithoutSubProperties()) Then

                    'Else
                    Dim childsXml As New StringBuilder
                    For Each child As Object In DirectCast(value, Array)
                      childsXml.Append(ToXmlString(Of Object)(child, processedInstances))
                    Next
                    objNode.AppendChild(newElement(pInfo.Name)).InnerXml = childsXml.ToString() '.Replace(Environment.NewLine, "")
                    'End If

                  Case GetType(IEnumerable).IsAssignableFrom(valueType)
                    'If (valueType.GetItemType().IsTypeWithoutSubProperties()) Then

                    'Else
                    Dim childsXml As New StringBuilder
                    For Each child As Object In DirectCast(value, IEnumerable)
                      childsXml.Append(ToXmlString(Of Object)(child, processedInstances))
                    Next
                    objNode.AppendChild(newElement(pInfo.Name)).InnerXml = childsXml.ToString() '.Replace(Environment.NewLine, "")
                    'End If

                  Case Else
                    objNode.AppendChild(newElement(pInfo.Name)).InnerXml = ToXmlString(Of Object)(value, processedInstances) '.Replace(Environment.NewLine, "")

                End Select

              End If


            End If
          Catch ex As Exception
            objNode.AppendChild(newElement(pInfo.Name)).InnerXml = ex.Message
          End Try
        End If
      Next

      processedInstances.Add(instance)

      Dim retStr As String = xDoc.ToFormattedString()
      Return retStr
    End If
  End Function




  <Extension(), EditorBrowsable(EditorBrowsableState.Always)>
  Public Function ToFormattedString(xDoc As XmlDocument) As String

    Dim settings As New XmlWriterSettings
    With settings
      .Indent = True
      .IndentChars = "  "
      .NewLineHandling = NewLineHandling.Replace
      .OmitXmlDeclaration = True
    End With

    Dim sb As New StringBuilder
    Dim xw As XmlWriter = XmlWriter.Create(sb, settings)

    xDoc.Save(xw)
    Return sb.ToString()
  End Function


  <Extension(), EditorBrowsable(EditorBrowsableState.Always)>
  Public Function IsTypeWithoutSubProperties(type As Type) As Boolean

    Select Case True
      Case type.IsValueType : Return True
      Case GetType(Integer).IsAssignableFrom(type) : Return True
      Case GetType(Int32).IsAssignableFrom(type) : Return True
      Case GetType(Int16).IsAssignableFrom(type) : Return True
      Case GetType(Int64).IsAssignableFrom(type) : Return True
      Case GetType(String).IsAssignableFrom(type) : Return True
      Case GetType(Boolean).IsAssignableFrom(type) : Return True
      Case GetType(DateTime).IsAssignableFrom(type) : Return True
      Case GetType(Guid).IsAssignableFrom(type) : Return True
      Case GetType(Version).IsAssignableFrom(type) : Return True
      Case GetType(Decimal).IsAssignableFrom(type) : Return True
      Case GetType(Double).IsAssignableFrom(type) : Return True
      Case GetType(Byte).IsAssignableFrom(type) : Return True
    End Select
    Return False
  End Function

End Module
