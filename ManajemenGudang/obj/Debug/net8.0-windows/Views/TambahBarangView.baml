   M S B A M L   `   `   `  ����J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  5   	   ,    &ManajemenGudang.Views.TambahBarangView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7vm(clr-namespace:ManajemenGudang.ViewModels     5   	     � Title$  Tambah Barang Baru��5   	   $	��450��6$   $	��400��61    � WindowStartupLocation$ CenterScreen=�6=    � SizeToContent$ Height=�6b    � DataContext 5      6   0ManajemenGudang.ViewModels.TambahBarangViewModel  5   
   .��5	      �� $��16,16,16,16q�6   .+�5
   
   �� $��Add Product��6   $��24(�6'   $
��Bold"�65   �� 5   
   $
��Wrap=�6   $3��-Enter product details to grow your inventory.��6(   $��14(�6]   ���	#FFA4A4A4$��0,0,0,24q�6�   �� 5   
   $��Product Name��6   $��0,0,0,4q�6(   $��14(�69   �� 5   
   $��28��6X   $��Center=�6d   $��14(�6�   $��0,0,0,12q�6�   $��4,0,0,0q�6�   ��6   �� *NamaBarangInput+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Description��6   $��14(�6'   $��0,0,0,4q�65   �� 5   
   $��64��6W    �� TextWrapping$
 Wrap=�6c    �� AcceptsReturn$
 True��6w   $��14(�6�   $��0,0,0,12q�6�   $��4,0,0,0q�6�   ��6   �� *DeskripsiInput+$ PropertyChanged=�6�   �� 5   
   $��Count��6   $��0,0,0,4q�6!   $��14(�62   �� 5   
   $��28��6T   $��14(�6`   $��Center=�6n   $��0,0,0,16q�6�   $��8,0,0,0q�6�   $��Stretch=�6�   ��6   �� *JumlahInput+$ PropertyChanged=�6�   �� 5   
   $��Add Product��6   $��0,0,0,0q�6'    �� 	IsDefault$
 True��69   ���	#FF4169B2$��14(�6�   $��SemiBold"�6�   ���	#FF4169B2
�������$��0,0,0,8q�6�   $��32��6�   ��6J   �� *TambahBarangCommand+6�   �� 5   
   $
��Back��6   $��0,0,0,0q�6    
����������	#FF4169B2$��14(�6�   $��32��6�   ���	#FF4169B2$��SemiBold"�6�   ��62   �� *KembaliCommand+6�   5      5      