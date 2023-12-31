# Jt.Common.Tool

一些公共帮助类、扩展方法

## Extension

### HttpClientExtension

- Get请求

- Post请求

### ObjectExtension

- 实例对象序列化成json

- 深拷贝（反射）

- 深拷贝（序列化）

- 复制有相同属性的值到另一个实例

- 对列表元素操作，复制有相同属性的值到另一个实例

- 实例字符串类型字段填充空字符串

- byte数组转换为对应的16进制字符串，是否有空格

- byte数组转换为对应的16进制字符串，填充字符

- 将byte类型的转换为16进制字符串

- 判断list是否为null或者为空

- 判断list是否不为null并且不为空

### StringExtension

- 判断字符串是否为空或只有空格

- 判断字符串是否不为空或只有空格

- 反序列化为指定类型的实例

- MD5加密

### DateTimeJsonConverter

### ExpressionExtend

- 合并表达式 expr1 AND expr2

- 合并表达式 expr1 or expr2

- 表达式取非

### QueryableExtension

- 分页查询

### ServiceCollectionExtension

- 服务接口注入

## Helper

### AppDomainHelper

- 获取程序根目标

- 在根目录创建文件夹

### AssemblyHelper

- 获取程序集的所有方法

- 获取类型的所有方法

- 获取程序集存在指定特性的方法

- 获取类型存在指定特性的方法

- 获取程序集继承了指定类型的子类的类型

- 获取多个程序集继承了指定类型的子类的类型

- 获取程序集存在指定特性的类型

- 获取多个程序集存在指定特性的类型

### DateTimeHelper

- 获取时间戳Timestamp

- 时间戳Timestamp转换成日期

### EnumHelper

- 获取枚举的每一项

- 获取枚举值上的描述

- 枚举类型转换为List

### ExpressionHelper

- 构建表达式目录树

### FileInfoHelper

- 将文本写入文件

### NamedHelper

- 转化为下划线命名 user_name

- 转化为驼峰命名 userName

- 转化为Pascal命名 UserName

### ResSystemHelper

- 检测目录是否存在，若不存在则创建

- 获取去除拓展名的文件路径

- 获取父目录的路径信息

- 获取文件名称

- 获取指定路径和基础目录的相对路径

- 获取路径路径下所有文件信息

- 获取指定目录下所有文件列表

### RSAHelper

- RSA加密

- RSA解密

### SnowflakeHelper

- 生成雪花id

### ValidateHelper

- 验证输入字符串是否与模式字符串匹配

- 验证输入字符串是否与模式字符串匹配（有筛选条件）

- 返回字符串真实长度

- 校验用户名有效性

- 校验密码有效性

- 校验int有效性

- 校验是否数字字符串

- 校验是否整数字符串

- 校验是否整数字符串（带正负号）

- 校验是否是浮点数

- 校验是否是浮点数 （可带正负号）

- 校验是否有中文字符

- 获取含有中文字符串的实际长度

- 校验输入字母

- 验证身份证是否合法  15 和  18位两种

- 校验是否是邮件地址

- 校验邮编有效性

- 校验固定电话有效性

- 校验手机有效性

- 校验电话有效性（固话和手机 ）

- 校验Url有效性

- 校验IP有效性

- 校验域名有效性

- 校验是否为base64字符串

- 校验字符串是否是GUID

- 校验输入的字符是否为日期

- 校验输入的字符是否为日期时间

- 校验字符串最大长度，返回指定长度的串

- 校验是否为json

### ZipHelper

- 根据给的文件参数，自动进行压缩或解压缩操作

- 压缩所有文件为zip

- 压缩指定的文件或文件夹为zip

- 判断文件名中是否含有ignoreNames中的某一项

- 压缩所有文件files，为压缩文件zipFile, 以相对于BaseDir的路径构建压缩文件子目录，ignoreNames指定要忽略的文件或目录

- 解压文件 到指定的路径，可通过targeFileNames指定解压特定的文件
