# 算术习题生成器

## 题面

四则运算—初级

编写一个四则运算程序，满足如下要求：
1 题目要求： 100以内加减法（满足小学二年级数学口算需求）

2 使用参数控制生成题目的个数

3 每道题目中出现的运算符个数不超过2个
4 程序一次运行生成的题目不能重复，请思考关于重复的定义。生成的题目存入执行程序的当前目录下的Exercises.txt文件，格式如下：
    1. 四则运算题目1
    2. 四则运算题目2

5 在生成题目的同时，计算出所有题目的答案，并存入执行程序的当前目录下的Answers.txt文件，格式如下：
    1. 答案1
    2. 答案2

6 估计需求分析、设计、编码、测试各阶段时间，记录实际工作中各项工作时间花费，并列表进行对比。

## 约定

### 最大值与最小值

最大值和最小值表示操作数及运算过程中产生的中间值的范围

例如: 

> 1 + 6 + 10 - 10 = 7

若将最大值设置为 **10**, 则运算过程中产生的最大值为 **17**, 不符合要求.

+ 当不填写时默认为 **int** 的 (最小值 + 1) 或 最大值 (防止出现溢出)

### 对重复的判定

本程序对重复的判断采用后缀表达式形式判定, 即两个表达式的后缀表达式一致即认定为重复.

例如以下两个表达式将被视作重复: 

> ( 1 + 3 ) - 1

> 1 + 3 - 1

二者的后缀表达式形式均为: **1 3 + 1 -**

## 隐藏操作

通过修改.cofig文件可以支持乘除法和乘方运算, 对应设置项如下.

```xml
<setting name="AllowMultiply" serializeAs="String">
    <value>False</value>
</setting>
<setting name="AllowDivide" serializeAs="String">
    <value>False</value>
</setting>
<setting name="AllowPower" serializeAs="String">
    <value>False</value>
</setting>
```

## 已知问题

+ 运算符个数较多时生成速度较慢

+ 运算乘方运算时可能出现溢出

<!--[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-f059dc9a6f8d3a56e377f745f24479a46679e63a5d9fe6f495e02850cd0d8118.svg)](https://classroom.github.com/online_ide?assignment_repo_id=451611&assignment_repo_type=GroupAssignmentRepo)-->
