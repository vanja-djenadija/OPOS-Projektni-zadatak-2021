   M S B A M L   `   `   `  ����T  PGraphicalUserInterfaceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:_2_GraphicalUserInterfaceApp_2_GraphicalUserInterfaceApp  5   	   -    '_2_GraphicalUserInterfaceApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:_2_GraphicalUserInterfaceApp     5   	     � Title$  TaskScheduler Settings��5   	   $	��400��6(   $	��500��65    � 
ResizeMode$ NoResize=�6A   .��5	      � $��10 10 10 10q�6    � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%  '�       '�� N     '�� �     '	� �    �� 5      " �6   .�5      ��  � �� �#FDF7E26/   5      �� 5      " ��6   .�5      ��  � ��$ 40��6+   �� 5       �@��Orientation$ 
Horizontal=�60   �� 5       � ��$ 10 0 0 0q�6+   5      �� 5      " ��6   .�5      ��  � ��$ Segoe UI)�6/   �� 5       � ��$ 13(�6-   �� 5       � ��$	 250��6*   5       �� 5!      " 	�6   .�5"      ��  � ��$ Segoe UI)�6/   5#      5$      .+�5&   
   �� $��0��6   .+�5'      �� $��Task Scheduler Settings��6   $��	160 0 0 0q�66   $��15(�6I   5(      �� 5)   
   $��1��6   .+�5*      �� $��Maximum Number of Cores��6   �� 5+      -   5         ��Name$ coresCB��5+      $	��150��6(   $��5 5 5 5q�64   5,      �� 5-   
   $��2��6   .+�5.      �� $(��"Maximum Number of Concurrent Tasks��6   �� 5/      -   5         ��Name$ maxNoConcurrentTasksTB��5/      $	��150��67   $��5 5 5 5q�6C   50      �� 51   
   $��3��6   .+�52      �� $��Priority Scheduling��6   	� 53      -   5        	 	�Name$	 
priorityRB��53   +   
 d� 	IsChecked$

 TrueY�6    	� 	GroupName$ priority��6?   $��Priority��6T   $��5 5 5 5q�6g   	� 54      -   5        $	 nonPriorityRB��54      $ priority��61   $��Non-Priority��6G   $��5 5 5 5q�6^   55      �� 56   
   $��4��6   .+�57      �� $��Preemtive Scheduling��6   	� 58      -   5        $	 preemptiveRB��58   +   $

 TrueY�6   $ 
preemptive��6A   $��
Preemptive��6X   $��5 5 5 5q�6m   	� 59      -   5        $	 nonPeemptiveRB��59      $ 
preemptive��62   $��Non-Preemptive��6I   $��5 5 5 5q�6b   5:      �� 5;   
   $��5��6   $��Right=�6"   $��0 0 0 0q�6>   .+�5<      �� -   5         ��Name$ startBtn��5<      $��Start��6'   $	��100��67   $��0 0 20 0q�6C   ���	#FF1699BC
�������$
��Bold"�6�   5=      5>      5?      