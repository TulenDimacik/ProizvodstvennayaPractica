   M S B A M L   `   `   `  ����@  <Lotus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LotusLotus  5      #    Lotus.ProjectManagementChange   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Lotus     5      X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5	   	     N� Title$  TodoListAdd��5
   	   $	��450��6   $	��800��6*   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6    � ColumnDefinitions 5   
   �� 5      $
��0.1* �6   6.   �� 5       $��0.25* �6   6/   �� 5!      $��0.25* �6   6/   �� 5"      $��0.25* �6   6/   �� 5#      $��0.25* �6   6/   �� 5$      $��0.25* �6   6/   �� 5%      $
��0.1* �6   6.   .+�5)   
   �� -   5        $��btnBack��5)      $��
Назад��6    $��0��60   $��2��6@   $��0��6T   $�� 10 0 50 20q�6a   $��Center=�6w   $��Center=�6�   $��16(�6�   6�   �� 5*   
   -   5        $��header��5*      $��2��6   $��3��6.   $��0��6B   $��0 0 0 0q�6O   $%��Изменение задачи��6`   $��Center=�6{   $��Center=�6�   $��20(�6�   �� 5+   
   $��3��6   $��1��6    $��5��64   $��0 0 0 0q�6A   $��Описание��6R   $��Center=�6e   $��Center=�6�   $��16(�6�   �� 5,   
   -   5        $��Description��5,      $��2��6)   $��3��69   $��6��6M   $��6��6Z   $�� 0 0 0 0q�6k   $��16(�6~   �� 5-   
   $��2��6   $��1��6    $��3��64   $��0 0 0 0q�6A   $��Дедлайн��6R   $��Center=�6d   $��Center=�6�   $��16(�6�   (  "System.Windows.Controls.DatePicker  5.   
   -   5        $��deadline��5.      $��3��6%   $��1��65   $��3��6I   $��1��6V   $��0 0 0 0q�6g   $��16(�6y   $
��Left=�6�   $��Center=�6�   �� 5/   
   $��4��6   $��1��6    $��1��64   $��0 0 0 0q�6A   $��Выполнено��6R   $��Center=�6f   $��Center=�6�   $��16(�6�   �� 50   
   -   5        $
��name��50       �� 	MaxLength$ 40��6   $��2��6-   $��2��6=   $��1��6Q   $��0 0 0 0q�6^   $
��Left=�6p   $��Center=�6�   $��16(�6�   �� 51   
   $��1��6   $��1��6    $��1��64   $��0 0 0 0q�6A   $��Название��6R   $��Center=�6e   $��Center=�6�   $��16(�6�   �� 52   
   -   5        $��cbStatus��52      $��5��6$   $��1��64   $��1��6H   $��10 0 9 0q�6V   $
��Left=�6i   $��Center=�6�   $��16(�6�   �� 53   
   -   5        $��	btnChange��53      $��Изменить��6"   $��3��65   $��1��6E   $��12��6Y   $��
 10 2 10 2q�6g   $��Center=�6|   $��Center=�6�   $��16(�6�   6�   55      56      