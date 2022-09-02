#!/user/bin/python
# coding = utf-8

import os
import sys
import subprocess
import time
from persistent_Bundle_version import bundle_version_data 

cdn_tools_path = "Tools/cdn/gy_cdn_uploader-0.0.1-SNAPSHOT.jar"
server_data_path = "ServerData"

project_path = sys.argv[1]
platform_cfg =  sys.argv[2]
build_env =  sys.argv[3]
build_asset_or_apk = int(sys.argv[4])


def shell_run(task,executeMethod):
    # new thread to tail log file
    print ("running..." + executeMethod)

    process = subprocess.call(executeMethod, shell=True)
    if (process == 0):
        print (task + " success!!!")
        time.sleep(3)
    else:
        print (task + " failure!!!")
        time.sleep(3)
        exit(1)


def Upload_to_cdn():
    bundle_data = bundle_version_data(project_path)
    asset_ver_code = bundle_data.get_bundle_version("asset_bundle_vsersion", build_env)
    executeMethod = ""
    cdn_tools = os.path.join(project_path,cdn_tools_path)
    data_path = server_data_path + "/"+ platform_cfg
    team_name = "zoids"
    if build_env == "Test":
        executeMethod = ""
        data_path = data_path + "/" +"test/{0}".format(asset_ver_code)
        local_path_name = os.path.join(project_path,data_path)
        remote_path_name = "{0}/test".format(platform_cfg)
        executeMethod = "java -jar {0} --team_name {1} --remote_path_name {2} --local_path_name {3}".format(cdn_tools,team_name,remote_path_name,local_path_name)
    elif build_env == "PPE":
        executeMethod = ""
        data_path = data_path + "/" +"ppe/{0}".format(asset_ver_code)
        local_path_name = os.path.join(project_path,data_path)
        remote_path_name = "{0}/ppe".format(platform_cfg)
        executeMethod = "java -jar {0} --team_name {1} --remote_path_name {2} --local_path_name {3}".format(cdn_tools,team_name,remote_path_name,local_path_name)
    elif build_env == "Product":
        executeMethod = ""
        data_path = data_path + "/" +"product/{0}".format(asset_ver_code)
        local_path_name = os.path.join(project_path,data_path)
        remote_path_name = "{0}/product".format(platform_cfg)
        executeMethod = "java -jar {0} --team_name {1} --remote_path_name {2} --local_path_name {3}".format(cdn_tools,team_name,remote_path_name,local_path_name)
    shell_run("Upload_to_cdn",executeMethod)



if __name__ == "__main__":
    Upload_to_cdn()