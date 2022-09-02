import os

from const import proto_path

def loadProtoFromServer():
    protos = {}
    f = open(proto_path+'\msgid.def', 'r',encoding='UTF-8')
    f_info = f.readlines()
    for line in f_info:
        fields = line.split(' ')
        if len(fields) == 2:
            str = fields[1].replace('\n','')
            protos[fields[0]] = str
    return  protos    
def loadProto():
    protos = []
    
    for dir in os.listdir(proto_path):  # 遍历当前目录所有文件和目录
        child = os.path.join(proto_path, dir)  # 加上路径，否则找不到
        if os.path.isdir(child):  # 如果是目录，则继续遍历子目录的文件
            for file in os.listdir(child):
                if os.path.splitext(file)[1] == '.proto':  # 分割文件名和文件扩展名，并且扩展名为'proto'
                    file = os.path.join(child, file)  # 同样要加上路径
                    f = open(file, 'r',encoding='UTF-8')
                    f_info = f.readlines()  # 获取文件所有内容
                    f.close()
                    fileProtos = findProtos(f_info)
                    protos = protos +fileProtos
        elif os.path.isfile(child):  # 如果是文件，则直接判断扩展名
            if os.path.splitext(child)[1] == '.proto':
                f = open(child, 'r',encoding='UTF-8')
                f_info = f.readlines()
                f.close()
                fileProtos = findProtos(f_info)
                protos = protos + fileProtos

    return  protos

def findProtos(lines):
    fileProtos = []
    for line in lines:
        if "message" in line:
            line = line.strip()
            fields = line.split(' ')
            str = fields[1].replace('{','')
            fileProtos.append(str)
        # else:
        #     continue
    return  fileProtos