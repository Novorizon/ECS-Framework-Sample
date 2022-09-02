#!/user/bin/python
# coding = utf-8

import json
import os

class PersonJSONEncoder(json.JSONEncoder):
    def default(self,object):
        if isinstance(object,version_data):
            return {"data_name" : object.data_name,"ver_code" : object.ver_code,"env" : object.env}

# env 说明 由整形代替当作枚举使用 0 ：None， 1 ：PPE ，2 ：Product
class version_data():
    data_name = ""
    ver_code = 0
    env = 0
    def __init__(self):
        data_name = ""
        ver_code = 0
        env = 0

    def init_data(self,name,code,env):
        self.data_name = name
        self.ver_code = code
        self.env = env

    def init_data_2(self,object):
        self.data_name = object["data_name"]
        self.ver_code = object["ver_code"]
        self.env = object["env"]


class bundle_version_data:
    def __init__(self,project_path):
        self.__persistent_file_path = "Tools/BuildTools/BundleVersion.json"
        self.__bundle_version_list = list()
        self.__file_path = os.path.join(project_path, self.__persistent_file_path)
        self.__load_save_bundle_version()

    def __get_bundle_data(self,bundleName,env):
        for bundle_data in self.__bundle_version_list:
            if bundle_data.data_name == bundleName and bundle_data.env == env:
                return bundle_data
        return None

    def get_bundle_version(self,bundleName,env):
        data = self.__get_bundle_data(bundleName,env)
        if data != None:
            return data.ver_code
        return 0
    
    def set_budle_version(self,bundleName,env):
        data = self.__get_bundle_data(bundleName,env)
        if data != None:
            data.ver_code += 1
            self.__save_bundle_version()
    
    def print_bundle_info(self):
        for bundle_data in self.__bundle_version_list:
            print("bundleName  : " + bundle_data.data_name + " |  ver_code  : " + str(bundle_data.ver_code) + " |  env : " + str(bundle_data.env))

    def __reset_bundle_version(self):
        self.__bundle_version_list.clear()
        asset_test_data = version_data()
        asset_ppe_data = version_data()
        asset_product_data = version_data()

        app_test_data = version_data()
        app_ppe_data = version_data()
        app_product_data = version_data()

        update_asset_test_data = version_data()
        update_asset_ppe_data = version_data()
        update_asset_product_data = version_data()

        asset_test_data.init_data("asset_bundle_vsersion",0,"Test")
        asset_ppe_data.init_data("asset_bundle_vsersion",0,"PPE")
        asset_product_data.init_data("asset_bundle_vsersion",0,"Product")
        app_test_data.init_data("app_bundle_vsersion",0,"Test")
        app_ppe_data.init_data("app_bundle_vsersion",0,"PPE")
        app_product_data.init_data("app_bundle_vsersion",0,"Product")
        update_asset_test_data.init_data("update_asset_version",0,"Test")
        update_asset_ppe_data.init_data("update_asset_version",0,"PPE")
        update_asset_product_data.init_data("update_asset_version",0,"Product")
        self.__bundle_version_list.append(asset_test_data)
        self.__bundle_version_list.append(asset_ppe_data)
        self.__bundle_version_list.append(asset_product_data)
        self.__bundle_version_list.append(app_test_data)
        self.__bundle_version_list.append(app_ppe_data)
        self.__bundle_version_list.append(app_product_data)
        self.__bundle_version_list.append(update_asset_test_data)
        self.__bundle_version_list.append(update_asset_ppe_data)
        self.__bundle_version_list.append(update_asset_product_data)
        self.__save_bundle_version()


    def __load_save_bundle_version(self):
        self.__bundle_version_list.clear()
        if not os.path.exists(self.__file_path):
            self.__reset_bundle_version()
        else:
            with open(self.__file_path,'r') as file_:
                json_data = json.load(file_)
                for item_data in json_data:
                    bundle_data = version_data()
                    bundle_data.init_data_2(item_data)
                    self.__bundle_version_list.append(bundle_data)


    def __save_bundle_version(self):
        with open(self.__file_path,'w') as file_:
            json.dump(self.__bundle_version_list,file_,cls=PersonJSONEncoder)
            file_.close()
