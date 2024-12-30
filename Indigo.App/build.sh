$baseDir = pwd

rm -rf ./wwwroot/*

cd ../indigo/

npm i
npm run build

cd "./build/"
cp -rf ./* ../../Indigo.App/wwwroot/