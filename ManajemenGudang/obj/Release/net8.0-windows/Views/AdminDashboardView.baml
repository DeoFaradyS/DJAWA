   M S B A M L   `   `   `  ���� J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  G(clr-namespace:ManajemenGudang.ConvertersManajemenGudang.Converters  .    (ManajemenGudang.Views.AdminDashboardView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7vm(clr-namespace:ManajemenGudang.ViewModels     ?
converters(clr-namespace:ManajemenGudang.Converters       � Title$  Admin Dashboard��$	��600��$	��900�� � WindowStartupLocation$ CenterScreen=� � DataContext 8   2ManajemenGudang.ViewModels.AdminDashboardViewModel   � 	Resources :   4ManajemenGudang.Converters.NotNullToBooleanConverter   NotNullToBooleanConverter��%   &	          .��� $��15q� � RowDefinitions �� $
��Auto �$��93.163���� .+��� $��0��$��Log Out��$��Right=�$��	16,0,16,0q�$��28��$��14(����� *LogoutCommand+� $��0,93,0,0q�$��2�� � ColumnDefinitions �� $	��250 ��� $��* �.+�]� $��0��$��0,0,15,0q�.+��� $	��Top=�$��	User List��$��18(�$
��Bold"�$��0,0,0,10q���  �� DisplayMemberPath$ Username��$��14(����� *
AllUsers+ �� SelectedItem �� *SelectedUser+]� $��1��.+��� $	��Top=�$��18(�$
��Bold"�$��0,0,0,10q��� $�Product Owner:�� �� ��� *SelectedUser.Username+ �� FallbackValue$ (Pilih pengguna)���� $��Bottom=�	 �� Orientation$	 
Horizontal=�$��0,15,0,0q�{��� 
 �� Path$
 SelectedItem� �� 	Converter# [  .+��� $��Status update to:��$��Center=�$��14(��� $
��10,0q�$	��150������ *AvailableStatuses+ �� *SelectedStatus+�� -   $��Change Status��$
��10,5q�$��28�����	#FF00328A
���   ����	#FF4169B2 J� RenderTransformOrigin$ 
0.478,0.56&����� *UpdateStatusCommand+&   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��   CanUserAddRows$ False��   
IsReadOnly$
 True��$��14(����� *SelectedUserItems+ �� *SelectedBarang+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Product Name��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
NamaBarang+  $ Count��$
 Auto  �� *Jumlah+  $ Status��$ *  �� *Status+  $ 
Created At��$ *  �� *TanggalInput+ �� StringFormat$ 
dd-MM-yyyy��  $ 
Updated At��$ *  �� *TanggalUpdateStatus+$ dd-MM-yyyy HH:mm���� $
��Left=�$	��Top=�.+��� $��24(�$
��Bold"�$
��Left=�$��Center=���  � Language$ id-id�$�	Dashboard���� $
��Wrap=�$@��:Manage your system, monitor activity, and stay in control.��$��14(����	#FFA4A4A4$
��Left=�$	��376��