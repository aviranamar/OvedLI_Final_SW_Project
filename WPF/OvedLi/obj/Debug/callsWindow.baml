   M S B A M L   `   `   `  ����A  =OvedLi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:OvedLiOvedLi  5   	       OvedLi.callsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:OvedLi     5   	     � Title$  ניהול קריאות��5   	   $��613.085��6   $��928.341��6/    � Icon$A ;pack://application:,,,/OvedLi;component/Resources/users.ico��5	   	    � AllowsTransparency .  � WindowStyle$
 None=�6f   ��5
      �� 5   
    �� ImageSource$F @pack://application:,,,/OvedLi;component/Resources/background.jpg��6   .��5      � .+�5   
   �� -   5         ��Name$ callbyemployeecb��5      $
��Left=�6-   $��	44,77,0,0q�6H   $	��Top=�6[   $	��120��6s   �� 5   
   -   5         ��Name$ callsbyemployeelbl��5      $1��+בחר עובד - קריאות פתוחות��6,   $
��Left=�6O   $��	27,46,0,0q�6j   $	��Top=�6}   �� 5   
   -   5         ��Name$ callsbyemployeebtn��5      $
��Left=�6N   $��
69,114,0,0q�6i   $	��Top=�6}   $��75��6�    J� RenderTransformOrigin$ -0.535,-0.033&�6�   ��6�   Z� ��6�   Z� .��5      �� $=��7pack://application:,,,/OvedLi;component/Resources/v.png��6   $��64��6U   $��43��6a   5      �� 5   
   -   5        $ 	cancelbtn��5      $
��Left=�6<   $��843,114,0,0q�6W   $	��Top=�6l   $��75��6�   $ -0.535,-0.033&�6�   ��6�   Z� ��6�   Z� .��5      �� $=��7pack://application:,,,/OvedLi;component/Resources/x.png��6   $��64��6U   $��43��6a   5      �� 5   
   -   5        	 ��Name$	 sp��5      $
��Left=�6!   $	��219��6<   $��
44,264,0,0q�6I   $	��Top=�6]   $	��782��6u   5      �� 5   
   -   5        $ labelLogOut��5      $��
יציאה��6%   $
��Left=�65   $��
866,73,0,0q�6P   $	��Top=�6d   $ 2.436,0.423&�6|   �� 5   
   -   5        $ callsbyemployeelbl_close��5      $1��+בחר עובד - קריאות סגורות��62   $
��Left=�6U   $��
253,46,0,0q�6p   $	��Top=�6�   �� 5   
   -   5        $ callbyemployeecb_closecb��5      $
��Left=�65   $��
262,77,0,0q�6P   $	��Top=�6d   $	��120��6|   �� 5   
   -	   5        $  callsbyemployeebtn_closetb��5      $
��Left=�6^   $��295,114,0,0q�6y   $	��Top=�6�   $��75��6�   $ -0.535,-0.033&�6�   ��6�   Z� ��6�   Z� .��5      �� $��Resources/v.png��6   $��64��6-   $��43��69   5      �� 5   
   -
   5        $ labelnotech��5      $&�� קריאות ללא טכנאים��6%   $
��Left=�6A   $��
468,46,0,0q�6\   $	��Top=�6p   �� 5    
   -   5        $ 	notechbtn��5       $
��Left=�6<   $��490,114,0,0q�6W   $	��Top=�6l   $��75��6�   $ -0.535,-0.033&�6�   ��6�   Z� ��6�   Z� .��5!      �� $��Resources/v.png��6   $��64��6-   $��43��69   5"      �� 5#   
   -   5        $ 	searchbtn��5#      $
��Left=�6<   $��650,114,0,0q�6W   $	��Top=�6l   $��75��6�   $ -0.535,-0.033&�6�   ��6�   Z� ��6�   Z� .��5$      �� $��Resources/v.png��6   $��64��6-   $��43��69   5%      �� 5&   
   -   5        
 ��Name$
 serchtb��5&      $
��Left=�6#   $��23��6>   $��
630,77,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��120��6�   �� 5'   
   -   5        $ 	searchlbl��5'      $ ��חיפוש לפי מפתח��6#   $
��Left=�6<   $��
630,46,0,0q�6W   $	��Top=�6k   �� 5(   
   -   5         ��Name$ loadtimeclock��5(      $��0��6   $
��Left=�64   $��75��6O   $	��Top=�6\   $��84��6t   $E��?pack://application:,,,/OvedLi;component/Resources/sendClock.gif��6   $��
777,39,0,0q�6�   $ 1.483,0.831&�6�   5)      5*      