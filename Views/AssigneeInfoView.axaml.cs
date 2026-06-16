using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModels;

namespace ProjectManagementSystem.Views;

public partial class AssigneeInfoView : UserControl {
    public AssigneeInfoView() {
        InitializeComponent();
    }

    private void RemoveAssignee_Tapped(object sender, TappedEventArgs e) {
        if (sender is TextBlock { DataContext: AssigneeEntry entry } &&
            DataContext is ProjectCreationViewModel vm) {
            vm.RemoveAssigneeCommand.Execute(entry);
        }
    }

    private void ClearUserSearch() {
        UserSearchBox.Text = string.Empty;
    }

    protected override void OnDataContextChanged(EventArgs e) {
        base.OnDataContextChanged(e);
        if (DataContext is ProjectCreationViewModel vm) {
            vm.Assignees.CollectionChanged += (_, _) => ClearUserSearch();
        }
    }
}