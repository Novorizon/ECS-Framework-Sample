#!/user/bin/python
# coding = utf-8

import datetime
import os
import subprocess
import _thread
import sys
import time
import tail
from persistent_Bundle_version  import *


setting_file_path = "Setting.json"

unity_path = sys.argv[1]
project_path = sys.argv[2]
output_path = sys.argv[3]
project_ver = sys.argv[4]
package_name = sys.argv[5]
product_name = sys.argv[6]
create_symbols = "true" == sys.argv[7]
useiltocpp = "true" == sys.argv[8]
build_env = sys.argv[9]
platform_cfg = sys.argv[10]
build_asset_or_apk = int(sys.argv[11])
url_ip_address = sys.argv[12]
url_ip_port = sys.argv[13]
login_server_type = sys.argv[14]

unity_path = "\"{0}\"".format(unity_path)


def tail_thread(tail_file):
    print ("Wait for tail file ... %s" % tail_file)
    while True:
        if os.path.exists(tail_file):
            print ("Start tail file..... %s" % tail_file)
            break

    t = tail.Tail(tail_file)
    t.register_callback(unity_log_tail)
    t.follow(s=3)

def unity_log_tail(txt):
    print(txt)

def unity_run(task, executeMethod):
    global cur_log_path
    global log_name
    logFile = os.path.join(cur_log_path,log_name + task + ".log")
    # new thread to tail log file
    _thread.start_new_thread(tail_thread, (logFile, ))

    link = ' '
    cmd = [unity_path, '-quit', '-batchmode', '-projectPath', project_path, '-buildTarget', platform_cfg,'-logFile', logFile, '-executeMethod', executeMethod]
    build_cmd = link.join(cmd)
    print ("Unity running..." + build_cmd)

    process = subprocess.call(build_cmd, shell=True)
    if (process == 0):
        print (task + " success!!!")
        time.sleep(3)
    else:
        print (task + " failure!!!")
        time.sleep(3)
        exit(1)

def ChangeLoginServerType():
    print("login_server_type" + login_server_type)
    executeMethod = 'Game.Editor.PublishUtils.ChangeLoginServerType ' + login_server_type
    unity_run("ChangeLogin_server_type",executeMethod)

def pre_build():
    global start_time
    global data_time
    global cur_log_path
    global log_name
    global project_name
    global cur_output_path
    
    print('*'.center(64, '*'))
    print(' START BUILDING '.center(64, '*'))
    print('*'.center(64, '*'))

    start_time = datetime.datetime.now()
    data_time = start_time.strftime('%Y%m%d')
    cur_data_time_hms = start_time.strftime('%H%M%S')

    cur_output_path = os.path.join(output_path,data_time,cur_data_time_hms)
    if not os.path.exists(cur_output_path):
        os.makedirs(cur_output_path)
    cur_log_path = os.path.join(cur_output_path,"Log")
    if not os.path.exists(cur_log_path):
        os.makedirs(cur_log_path)

    os.chdir(project_path)
    
    log_name = "{0}_{1}_{2}".format(str(data_time) + str(cur_data_time_hms),package_name,build_asset_or_apk)
    if useiltocpp :
        isILMode = "ILToCpp"
    else:
        isILMode = "Mono"
    project_name ="{0}_{1}_{2}_{3}".format(package_name,str(data_time) + str(cur_data_time_hms),build_env,isILMode)
    ChangeLoginServerType()

def create_resources():
    executeMethod = ''
    if build_env == "Product":
        executeMethod = 'Game.Editor.PublishUtils.CreateResourcesProduct ' + str(url_ip_address) + ' ' + str(url_ip_port)
    elif build_env == "PPE":
        executeMethod = 'Game.Editor.PublishUtils.CreateResourcesPPE '+ str(url_ip_address) + ' ' + str(url_ip_port)
    elif build_env == "Test":
        executeMethod = 'Game.Editor.PublishUtils.CreateResourcesTest '+ str(url_ip_address) + ' ' + str(url_ip_port)
    unity_run("create_resources",executeMethod)

def build_assetBundle():
    global bundle_data
    executeMethod = ''
    asset_ver_code = bundle_data.get_bundle_version("asset_bundle_vsersion", build_env)
    if build_env == "Product":
        executeMethod = 'Game.Editor.PublishUtils.BuildAssetProduct ' + str(asset_ver_code)
    elif build_env == "PPE":
        executeMethod = 'Game.Editor.PublishUtils.BuildAssetPPE '+ str(asset_ver_code)
    elif build_env == "Test":
        executeMethod = 'Game.Editor.PublishUtils.BuildAssetTest '+ str(asset_ver_code)
    unity_run("build_assetBundle",executeMethod)
    create_resources()
    
def update_assetBundle():
    global bundle_data
    executeMethod = ''
    asset_ver_code = bundle_data.get_bundle_version("asset_bundle_vsersion", build_env)
    if build_env == "Product":
        executeMethod = 'Game.Editor.PublishUtils.UpdataBuildAssetProduct ' + str(asset_ver_code)
    elif build_env == "PPE":
        executeMethod = 'Game.Editor.PublishUtils.UpdataBuildAssetPPE ' + str(asset_ver_code)
    elif build_env == "Test":
        executeMethod = 'Game.Editor.PublishUtils.UpdataBuildAssetTest ' + str(asset_ver_code)
    unity_run("update_assetBundle",executeMethod)
    create_resources()
    bundle_data.set_budle_version("update_asset_version",build_env)

def build_apk():
    global project_name
    global bundle_data
    global cur_output_path
    executeMethod = ''
    project_full_name = os.path.join(cur_output_path,project_name)
    ver_code = bundle_data.get_bundle_version("app_bundle_vsersion", build_env)
    if platform_cfg == 'Android':
        executeMethod = 'Game.Editor.PublishUtils.BuildAndroidAPK ' + product_name + ' ' + package_name + ' ' + project_full_name+".apk" + ' ' + str(project_ver) + ' '  + str(ver_code) + ' ' + str(build_env) + ' ' + str(useiltocpp) + ' ' + str(create_symbols)
    elif platform_cfg == 'iOS':
        executeMethod = 'Game.Editor.PublishUtils.BuildIOSIPA ' +  project_full_name + ' ' + str(project_ver) + ' ' + str(build_env) + ' ' + str(useiltocpp)
    unity_run('build_apk', executeMethod)

def start_build():
    global bundle_data
    bundle_data = bundle_version_data(project_path)
    if build_asset_or_apk == 1:
        build_assetBundle()
        build_apk()
    elif build_asset_or_apk == 2:
        update_assetBundle()

    
    
def post_build():
    global start_time
    end_time = datetime.datetime.now()
    print('Totally use: ' + str(end_time - start_time))

    print('*'.center(64, '*'))
    print(' END BUILDING '.center(64, '*'))
    print('*'.center(64, '*'))


if __name__ == "__main__":
    pre_build()
    start_build()
    post_build()
