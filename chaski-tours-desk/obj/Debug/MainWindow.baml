   M S B A M L   `   `   `  ����L  Hchaski-tours-desk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:chaski_tours_desk.Componenteschaski_tours_desk.Componentes  5   	   G(clr-namespace:chaski_tours_desk.Ventanaschaski_tours_desk.Ventanas  5   	   "    chaski_tours_desk.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Ccomponentes+clr-namespace:chaski_tours_desk.Componentes     5   	   =ventanas(clr-namespace:chaski_tours_desk.Ventanas     5   	   -   5          � WindowState$  	Maximized=�5   0    � Title$ 
MainWindow��5	   	   $	��720��6   $
��1280��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ Fondo��5      $��Images/conejo.jpg��6   $��UniformToFill=�6:   $��Center=�5      �� 5   
   
���   � J� Opacity$ .5Q�6!   +   %chaski_tours_desk.Ventanas.UserLayout  5   
   -   5          Name$ usuario��5      $y�Visible=�6/   .   (chaski_tours_desk.Ventanas.LogSignWindow  5   
   -   5          Name$ logSign��5   !   $y�	Collapsed=�62   5      ,   &chaski_tours_desk.Ventanas.AdminLayout  5   
   -   5          Name$ admin��5      $y�	Collapsed=�6   5      5      