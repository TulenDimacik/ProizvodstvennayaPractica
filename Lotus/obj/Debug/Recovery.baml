   M S B A M L   `   `   `  ����@  <Lotus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LotusLotus  5          Lotus.Recovery   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Lotus     5        N� Title$  Recovery��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.6* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.6* �6   �� 5      $
��0.6* �6   �� 5      $
��1.4* �6    � ColumnDefinitions 5   
   �� 5      $��0.25* �6   6/   �� 5      $��0.25* �6   6/   �� 5      $��0.25* �6   6/   �� 5      $��0.25* �6   6/   �� 5      $��0.25* �6   6/   .+�5   
   �� $��1��6   $��3��6    $��1��64   $��0 0 0 0q�6A   $1��+Восстановление доступа��6R   $��Center=�6s   $��Center=�6�   $��30(�6�   �� 5   
   -   5        $	��lab��5      $��1��6   $��1��6+   $��2��6?   $��0 0 0 0q�6L   $��
Почта��6]   $��Center=�6m   $��Center=�6�   $��16(�6�   �� 5   
   -   5        $��btnSendCode��5   '   $��2��6:   $��2��6J   $��3��6^   $��40 10 40 10q�6k   $��Отправить код��6�   $��Center=�6�   $��Center=�6�   $��16(�6�   �� 5    
   -   5        $��
btnConfirm��5    ;   $y�Hidden=�6'   $��2��6M   $��2��6]   $��3��6q   $��40 10 40 10q�6~   $��Подтвердить��6�   $��Center=�6�   $��Center=�6�   $��16(�6�   �� 5!   
   -   5        $��btnBack��5!   '   $��0��66   $��1��6F   $��0��6Z   $��	0 0 10 10q�6g   $��
Назад��6z   $��Center=�6�   $��Center=�6�   $��16(�6�   �� 5"   
   -   5        $
��Mail��5"   !    �� 	MaxLength$ 50��6   $��2��6-   $��2��6=   $��2��6Q   $��10 5 9 5q�6^   $
��Left=�6q   $��Center=�6�   $��16(�6�   �� 5#   
   -   5        $
��Code��5#   5   $y�Hidden=�6   $ 20��6&   $��2��6A   $��2��6Q   $��2��6e   $��10 5 9 5q�6r   $
��Left=�6�   $��Center=�6�   $��16(�6�   5$      5%      