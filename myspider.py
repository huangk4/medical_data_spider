import requests
import re
import time
import pymssql
import os
import datetime

hnurl="http://dc.hn-fda.gov.cn/qx/qxProdInfoActionWZ!cxlist.do?queryBean.pn="
jsurl="http://218.94.26.170:9080/datacenter/dc/list/bc757b6cd64a40939ee7ae2199999832?pageNo="

conn=pymssql.connect(host='140.143.230.185',
                       user='sa',
                       password='huang@123456',
                       database='SpiderData',
                       charset='utf8')

#查看连接是否成功
cursor = conn.cursor()

#显示爬取的数据总数
nums=0;




#获取江苏省医疗器械页面中所有URL链接
def JSgeturl(text):
    result=re.search("<tbody>(.*?)</tbody>",text,re.S)   
    tableresult=result.group(1)
    result2=re.findall("<tr>.*?href=\"(.*?)\"",tableresult,re.S)
    return result2

#获取湖南省医疗器械页面中所有URL链接
def HNgeturl(text):
    result=re.search("<table id=\"l(.*?)</table>",text,re.S)   
    tableresult=result.group(1)
    result2=re.findall("<tr>.*?href=\"(.*?)\"",tableresult,re.S)
    for  i in range(len(result2)):
        result2[i]="http://dc.hn-fda.gov.cn/qx/"+result2[i]
    # print(result2)
    # input()
    return result2


#获取网站返回内容，超时重连20次
def gettext(url):
    time.sleep(0.2)
    for i in range(1, 20):
        try:
            result=requests.get(url,timeout=2)
            break
        except requests.exceptions.Timeout:
            print (url+"请求超时，第"+'%d'%i+"次重复请求")
            continue
        except requests.exceptions.ConnectionError:
            print (url+"请求错误，第"+'%d'%i+"次重复请求")
            continue
    if result.status_code == 200:
        return result.text
    else:
        print(url+"获取数据网站多次失败")
        quit()


#获取江苏医疗器械数据并插入数据库
def JSgetdata(text):
    result1=re.search("<tbody>(.*?)</tbody>",text,re.S)   
    tableresult=result1.group(1) 
    result2=re.findall("<th.*?>(.*?)</th>.*?>(.*?)</td>",tableresult,re.S)
    sql="insert into JSdata (产品名称,单位名称,注册证号,管理类别,规格型号,"\
    "有效日期,发证日期,发证机关) values ("
    for datas in result2:
        #print(datas[1])
        sql=sql+"'"+datas[1].replace("'","''")+"',"
    sql=sql[:-1]
    sql=sql+")"
    try:
        cursor.execute(sql)
    except pymssql.OperationalError:
        print(sql)
        quit()
    except pymssql.ProgrammingError:
        print(sql)
        quit()
    global nums
    nums=nums+1
    print(nums)

#获取湖南医疗器械数据并插入数据库
def HNgetdata(text):
    result1=re.findall("<td.*?>(.*?)</td>",text,re.S)   
    sql="insert into HNdata (产品中文名称,产品英文名称,产品注册号,产品类别,有效期起,"\
    "有效期止,生产厂家,注册地址,规格型号,注册类别,产品性能结构及组成,产品使用范围,其他内容,备注) values ("
    t_num=0
    #删除最后一个非数据元素
    result1.pop()
    for datas in result1:
        t_num=t_num+1
        #本列数据可能含特殊字符，需重新正则匹配
        if t_num==11:
            if re.search("\'>",datas):
                sql=sql+"'"+re.search(".*\'>(.*)",datas).group(1).replace("'","''")+"',"
                continue
        sql=sql+"'"+datas.replace("'","''")+"',"
    sql=sql[:-1]
    sql=sql+")"
    try:
        cursor.execute(sql)
    except pymssql.OperationalError:
        print(sql)
        quit()
    except pymssql.ProgrammingError:
        print(sql)
        quit()
    global nums
    nums=nums+1
    print(nums)



if __name__ == "__main__":

    print("请输入需要爬取的省份，“湖南”或者“江苏”（默认湖南）")
    province=input();
    print("请输入开始页数")
    datafirst=input();
    datafirst=int(datafirst)
    print("请输入数据列表数")
    datanums=input();
    datanums=int(datanums)
    filename=province+time.strftime("%H.%M.%S")+".txt"
    if province=="江苏":
        #爬取江苏省医疗器械数据
        for i in range(datafirst, datanums+1):
            url=jsurl+str(i)
            urltext=gettext(url)
            urllist=JSgeturl(urltext)
            for anyurl in urllist:
                urltext=gettext(anyurl)
                JSgetdata(urltext)
            conn.commit()
            print("已完成江苏省第"+str(i)+"页医疗据爬取")
            myfile=open(filename,"a")
            myfile.write(url+'\n')
            myfile.close()
            

    else:
        #爬取江苏省医疗器械数据
        for i in range(datafirst,datanums+1):
            url=hnurl+str(i)+"&queryBean.pageSize=10"
            urltext=gettext(url)
            urllist=HNgeturl(urltext)
            for anyurl in urllist:
                urltext=gettext(anyurl)
                HNgetdata(urltext)
            conn.commit()
            print("已完成湖南省第"+str(i)+"页医疗据爬取")
            myfile=open(filename,"a")
            myfile.write(url+'\n')
            myfile.close()
        #undo: 写入文件中







