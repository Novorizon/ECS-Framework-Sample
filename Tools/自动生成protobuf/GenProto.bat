protoc --proto_path=./Proto/src  --csharp_out=../../Unity/Assets/Scripts/Net/proto def.proto
protoc --proto_path=./Proto/src  --csharp_out=../../Unity/Assets/Scripts/Net/proto struct.proto
protoc --proto_path=./Proto/src  --csharp_out=../../Unity/Assets/Scripts/Net/proto req.proto
protoc --proto_path=./Proto/src  --csharp_out=../../Unity/Assets/Scripts/Net/proto ack.proto
protoc --proto_path=./Proto/src  --csharp_out=../../Unity/Assets/Scripts/Net/proto login.proto
::protoc --proto_path=./Proto/src --go_out=../../Server/src/server/msg ./Proto/src/*.proto

python3 gen_proto.py