   M S B A M L   `   `   `  ����J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  5   	   G(clr-namespace:ManajemenGudang.ConvertersManajemenGudang.Converters  5   	   .    (ManajemenGudang.Views.AdminDashboardView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7vm(clr-namespace:ManajemenGudang.ViewModels     5   	   ?
converters(clr-namespace:ManajemenGudang.Converters     5   	     � Title$  Admin Dashboard��5   	   $	��600��6!   $	��900��6.    � WindowStartupLocation$ CenterScreen=�6:    � DataContext 5
      8   2ManajemenGudang.ViewModels.AdminDashboardViewModel  5   
    � 	Resources 5      :   4ManajemenGudang.Converters.NotNullToBooleanConverter   NotNullToBooleanConverter��%   &	          5   
   6/   5      .��5      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   $��93.163��6*   �� 5      .+�5   
   �� $��0��6   $��Log Out��5      $��Right=�5      $��	16,0,16,0q�5      $��28��6%   $��14(�61   ��5      �� *LogoutCommand+5   1   � 5   
   $��0,93,0,0q�6   $��2��6!    � ColumnDefinitions 5      �� 5       $	��250 �6#   �� 5!      $��* �6#   .+�5$      ]� $��0��6   $��0,0,15,0q�6(   .+�5%      �� $	��Top=�6   $��	User List��61   $��18(�6B   $
��Bold"�6P   $��0,0,0,10q�6b   �� 5&       �� DisplayMemberPath$ Username��5(      $��14(�67   ��5&      �� *
AllUsers+ �� SelectedItem 5'      �� *SelectedUser+5(   7   5)      ]� 5+      $��1��6   .+�5,      �� $	��Top=�6   $��18(�61   $
��Bold"�6?   $��0,0,0,10q�6Q   �� 5-      $�Product Owner:��6    �� 5.      �6   �� *SelectedUser.Username+ �� FallbackValue$ (Pilih pengguna)��5/      �� 51      $��Bottom=�6   	 �� Orientation$	 
Horizontal=�65   $��0,15,0,0q�6N   {�52      �� 
 �� Path$
 SelectedItem� �� 	Converter# [  .+�53      �� $��Status update to:��6    $��Center=�69   $��14(�6T   �� 54      $
��10,0q�6   $	��150��6-   ��55      �� *AvailableStatuses+ 56      �� *SelectedStatus+�� 57      -   5        $��Change Status��57      $
��10,5q�65   $��28��6l   ���	#FF00328A
���   ����	#FF4169B2 J� RenderTransformOrigin$ 
0.478,0.56&�6�   ��6D   �� *UpdateStatusCommand+6�   58      &   System.Windows.Controls.DataGrid  5:         AutoGenerateColumns$ False��5<         CanUserAddRows$ False��67      
IsReadOnly$
 True��6N   $��14(�6`   ��5:      �� *SelectedUserItems+ 5;      �� *SelectedBarang+   Columns 5=      0  *System.Windows.Controls.DataGridTextColumn  5>      ,  &System.Windows.Controls.DataGridColumn   Header$ Product Name��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
NamaBarang+6b     5?      $ Count��6-   $
 Auto 6W    6<   �� *Jumlah+6W     5@      $ Status��6-   $ * 6X    6=   �� *Status+6X     5A      $ 
Created At��6-   $ * 6}    6A   �� *TanggalInput+ �� StringFormat$ 
dd-MM-yyyy��6}     5B      $ 
Updated At��6-   $ * 6�    6A   �� *TanggalUpdateStatus+$ dd-MM-yyyy HH:mm��6�   5D      5E      5F      �� 5G   
   $
��Left=�6   $	��Top=�60   .+�5H      �� $��24(�6   $
��Bold"�6&   $
��Left=�68   $��Center=�6S   �� 6o    � Language$ id-id�6s   $�	Dashboard��6�   6�   �� 5I      $
��Wrap=�6   $@��:Manage your system, monitor activity, and stay in control.��6,   $��14(�6n   ���	#FFA4A4A4$
��Left=�6�   $	��376��6�   5J      5K      5L      