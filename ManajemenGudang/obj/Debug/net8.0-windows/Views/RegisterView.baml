   M S B A M L   `   `   `  ����J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  5   	   (    "ManajemenGudang.Views.RegisterView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7vm(clr-namespace:ManajemenGudang.ViewModels     5   	     � Title$  Registrasi Akun Gudang��5   	   $	��350��6(   $	��400��65    � WindowStartupLocation$ CenterScreen=�6A    � DataContext 5      2   ,ManajemenGudang.ViewModels.RegisterViewModel  5   
   .��5	      �� $��16,16,16,16q�6   .+�5
   
   �� $��Create an Account��6   $��24(�6-   $
��Bold"�6;   $��Center=�6M   �� 5   
   $
��Wrap=�6   $3��-Please login to continue managing your items.��6(   $��Center=�6]   $��Center=�6x   $��0,0,0,24q�6�   $��14(�6�   ���	#FFA4A4A46�   �� 5   
   $��Username��6   $��0,0,0,4q�6$   $��14(�65   �� 5   
   $��0,0,0,12q�6Q   $��28��6c   $��Center=�6o   $��14(�6�   $��4,0,0,0q�6�   ��6   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Password��6   $��0,0,0,4q�6$   $��14(�65   G� 5   
   -   5         G�Name$ PasswordInput��5      $��0,0,0,16q�6-   $��28��6?   $��Center=�6K   $��14(�6f   $��4,0,0,0q�6t   �� 5   
   $��Register��6   $��-2,0,0,0q�6$    �� 	IsDefault$
 True��67   ���	#FF4169B2
�������$��14(�5   s   $��SemiBold"�6�   ���	#FF4169B2$��0,0,0,16q�6�   $��32��6�   ��5      �� *RegisterCommand+��5      ��  �� ElementName$ PasswordInput��6�   �� 5   
   $)��#Already have an account? Login here��6    c� ButtonStyleKey#��[" 5      $��0,0,0,0q�6H   ��5      �� *BackToLoginCommand+5   H   5      5      