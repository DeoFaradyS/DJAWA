   M S B A M L   `   `   `  ����J  FManajemenGudang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ManajemenGudang.ViewModelsManajemenGudang.ViewModels  5   	   %    ManajemenGudang.Views.LoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7vm(clr-namespace:ManajemenGudang.ViewModels     5   	     � Title$  Login Gudang��5   	   $	��350��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�67    � DataContext 5      /   )ManajemenGudang.ViewModels.LoginViewModel  5   
   .��5	      �� $��16,16,16,16q�6   .+�5
   
   �� $��Welcome Back��6   $��24(�6(   $
��Bold"�66   $��Center=�6H   �� 5   
   $
��Wrap=�6   $3��-Please login to continue managing your items.��6(   $��Center=�6]   $��0,0,0,24q�6z   $��14(�6�   ���	#FFA4A4A46�   �� 5   
   $��Username��6   $��0,0,0,4q�6$   $��14(�65   �� 5   
   $��0,0,0,12q�6Q   $��28��6c   $��14(�6o   $��Center=�6}   $��4,0,0,0q�6�   ��6   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Password��6   $��0,0,0,4q�6$   $��14(�65   G� 5   
   -   5         G�Name$ LoginPasswordBox��5      $��0,0,0,16q�60   $��14(�6B   $��28��6P    G� PasswordChar$ *��6\   $��Center=�6m   $��4,0,0,0q�6�   �� 5   
   -   5        $��Login��5      $��0,0,0,0q�6!    �� 	IsDefault$
 True��63   $��Center=�5   `   $��32��6�    � UseLayoutRounding$ False��6�   $��Center=�6�   $��Center=�6�   $��14(�6�   $��SemiBold"�6�   ���	#FF4169B2
����������	#FF4169B2$��0,0,0,16q�6M  ��5      �� *LoginCommand+��5      ��  �� ElementName$ LoginPasswordBox��6M  �� 5   
   $,��&Don’t have an account? Register here��6   	 c� ButtonStyleKey#��["	 5      $��0,0,0,0q�6L   ��5      �� *OpenRegisterViewCommand+5   L   5      5      