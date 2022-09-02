from MsgID.csfile import genCSfile
from MsgID.gofile import genGolangfile
from MsgID.proto import loadProtoFromServer

protos = loadProtoFromServer()
genCSfile(protos)
#genGolangfile(protos)






