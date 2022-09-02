import os
import sys


build_type = int(sys.argv[1])
target_platfrom = sys.argv[2]

hot_update_file_path = "Assets/AddressableAssetsData/{0}/addressables_content_state.bin".format(target_platfrom)

def upload_hot_update_file():
    if build_type == 1:
        excute_cmd = "git add " + str(hot_update_file_path)
        os.system(excute_cmd)
        excute_cmd = "git commit -m \"upload by jenkins addressable .bin file.\""
        os.system(excute_cmd)
        excute_cmd = "git checkout ."
        os.system(excute_cmd)
        excute_cmd = "git clean -df"
        os.system(excute_cmd)
        excute_cmd = "git fetch -p"
        os.system(excute_cmd)
        excute_cmd = "git pull --rebase"
        os.system(excute_cmd)
        excute_cmd = "git push"
        os.system(excute_cmd)

if __name__ == "__main__":
    upload_hot_update_file()