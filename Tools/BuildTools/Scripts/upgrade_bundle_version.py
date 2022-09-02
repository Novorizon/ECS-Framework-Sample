#!/user/bin/python
# coding = utf-8


import sys
from persistent_Bundle_version import bundle_version_data

build_asset_or_apk = int(sys.argv[1])
build_env = sys.argv[2]
project_path = sys.argv[3]

if __name__ == "__main__":
    bundle_data = bundle_version_data(project_path)
    if build_asset_or_apk == 1:
        bundle_data.set_budle_version("asset_bundle_vsersion",build_env)
        bundle_data.set_budle_version("app_bundle_vsersion",build_env)
