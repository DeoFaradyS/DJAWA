   M S B A M L   `   `   `  ����J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  5   	   -    'ManajemenGudang.Views.RiwayatBarangView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7vm(clr-namespace:ManajemenGudang.ViewModels     5   	     � Title$  Riwayat Barang��5   	   $	��600��6    $	��800��6-    � WindowStartupLocation$ CenterScreen=�69    � DataContext 5      7   1ManajemenGudang.ViewModels.RiwayatBarangViewModel  5   
   .��5	      �� $��16,16,16,16q�6   .+�5
   
   �� $��0,0,0,24q�6   $
��Left=�6'   .+�5      �� $��Product List��6   $��24(�6,   $
��Bold"�6:   $
��Left=�6L   $��Center=�6g   �� 5      $
��Wrap=�6   $-��'Browse all products currently in stock.��6,   $��14(�6[   ���	#FFA4A4A4$
��Left=�6�   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��0,0,0,24q�6.   .+�5      �� $ 
Horizontal=�5      .+�5      �� $	��368��5      $��4,0,0,0q�6b   $��28��6t   $��14(�6�   $��0,0,8,0q�6�   $��Center=�6�   $��Center=�6�   ��6!   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      $��80��5      $��Search��6    
�������$��14(�6j   $��SemiBold"�6x   ���	#FF4169B2���	#FF4169B2$��28��6�   ��61   �� *CariBarangCommand+6�   5      �� 5      $��Add Product��5   A   $��28��6W   $��14(�6c   $��SemiBold"�6q   ���	#FF4169B2���	#FF4169B2
�������$��	16,0,16,0q�6�   $��Center=�6�   $��Right=�6�   $��	178,0,0,0q�6  ��6   �� *OpenTambahBarangViewCommand+6  5      &   System.Windows.Controls.DataGrid  5   
   -   5           
IsReadOnly$
 True��5   8      AutoGenerateColumns$ False��6J   $��0,0,0,29q�6�   $��Stretch=�6�   $��Stretch=�6�   $	��329��6�   ��6   �� *DaftarBarang+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Product Name��5   $   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6:   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6   �� *
NamaBarang+6:     5       $ Description��5!   #   $	 2* 68   
 6   �� *	Deskripsi+68     5"      $ Count��5#       $
	 Auto 6/   
 6   �� *Jumlah+6/     5$      $ Status��5%       $	 * 60   
 6   �� *Status+60     5&      $ 
Created At��5'   ?   $	 * 6S   
 6   �� *TanggalInput+ �� StringFormat$ 
dd-MM-yyyy��6S   5)      �� 5*   
   $��Logout��5,   +   $
��10,5q�6<   $��Center=�6K   $��Right=�6o   ��6	   �� *LogoutCommand+6o   5-      5.      