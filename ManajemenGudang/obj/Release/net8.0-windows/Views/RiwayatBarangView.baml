   M S B A M L   `   `   `  ���� J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  -    'ManajemenGudang.Views.RiwayatBarangView   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     7vm(clr-namespace:ManajemenGudang.ViewModels       � Title$  Riwayat Barang��$	��600��$	��800�� � WindowStartupLocation$ CenterScreen=� � DataContext 7   1ManajemenGudang.ViewModels.RiwayatBarangViewModel  .���� $��16,16,16,16q�.+��� $��0,0,0,24q�$
��Left=�.+��� $��Product List��$��24(�$
��Bold"�$
��Left=�$��Center=��� $
��Wrap=�$-��'Browse all products currently in stock.��$��14(����	#FFA4A4A4$
��Left=���  �� Orientation$ 
Horizontal=�$��0,0,0,24q�.+��� $ 
Horizontal=�.+��� $	��368��$��4,0,0,0q�$��28��$��14(�$��0,0,8,0q�$��Center=�$��Center=����� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� $��80��$��Search��
�������$��14(�$��SemiBold"����	#FF4169B2���	#FF4169B2$��28������ *CariBarangCommand+�� $��Add Product��$��28��$��14(�$��SemiBold"����	#FF4169B2���	#FF4169B2
�������$��	16,0,16,0q�$��Center=�$��Right=�$��	178,0,0,0q����� *OpenTambahBarangViewCommand+&   System.Windows.Controls.DataGrid  -      
IsReadOnly$
 True��   AutoGenerateColumns$ False��$��0,0,0,29q�$��Stretch=�$��Stretch=�$	��329������ *DaftarBarang+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Product Name��	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
NamaBarang+  $ Description��$	 2* 
 �� *	Deskripsi+  $ Count��$
	 Auto 
 �� *Jumlah+  $ Status��$	 * 
 �� *Status+  $ 
Created At��$	 * 
 �� *TanggalInput+ �� StringFormat$ 
dd-MM-yyyy���� $��Logout��$
��10,5q�$��Center=�$��Right=����� *LogoutCommand+