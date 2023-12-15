Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public Interface IAfsCompositionInjector
  Inherits IDisposable

  Function GetVersion() As Version

  ReadOnly Property SourceProviderFactories() As IEnumerable(Of IAfsSourceProviderFactory)

  ReadOnly Property SourceProviderFactories(identifier As Guid) As IAfsSourceProviderFactory




  ReadOnly Property PipeProviderFactories() As IEnumerable(Of IAfsPipeProviderFactory)
  ReadOnly Property PipeProviderFactories(identifier As Guid) As IAfsPipeProviderFactory



  ReadOnly Property ServerFactories() As IEnumerable(Of IAfsServerFactory)
  ReadOnly Property ServerFactories(identifier As Guid) As IAfsServerFactory





End Interface
