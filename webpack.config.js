const path = require("path");
const webpack = require("webpack");
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

var out_path = path.resolve("./public");


var babelOptions = {
  presets: [
      ["@babel/preset-env", {
          "targets": {
              "browsers": ["last 2 versions"]
          },
          "modules": false
      }]
  ]
};

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

var commonPlugins = [
    new HtmlWebpackPlugin({
        filename: out_path + '/index.html',
        template: path.resolve('./index.html')
    })
];

var cfg = {
  entry: { app: [
             "@babel/polyfill",
             path.resolve('./src/Starter.fsproj') ],
           style: [
             path.resolve('./node_modules/react-toolbox/lib/_colors.scss'),
             path.resolve('./src/theme/_config.scss'),
             path.resolve('./src/theme/AppBarTheme.scss') ] },
  mode: isProduction ? "production" : "development",
  output: {
      publicPath: "/",
      path: out_path,
      filename: isProduction ? '[name].[hash].js' : '[name].js'
  },
  optimization : {
    splitChunks: {
        cacheGroups: {
            commons: {
                test: /[\\/]node_modules[\\/]/,
                name: "vendors",
                chunks: "all"
            },
            fable: {
                test: /[\\/]fable-core[\\/]/,
                name: "fable",
                chunks: "all"
            }
        }
    },
  },
  plugins: isProduction ?
      commonPlugins.concat([
          new MiniCssExtractPlugin({
              filename: 'style.scss'
          })
      ])
      : commonPlugins.concat([
          new webpack.HotModuleReplacementPlugin(),
          new webpack.NamedModulesPlugin()
      ]),
  devServer: {
    contentBase: out_path,
    publicPath: "/",
    port: 9090,
    hot: true,
    inline: true
  },
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: {
            define: isProduction ? [] : ["DEBUG"],
            extra: { optimizeWatch: true }
          }
        }
      },      
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        },
      },
      {
        test: /\.s?[ac]ss$/,
        use: [
            'style-loader',
            { loader: 'css-loader',
              options: {
                  modules: true, // default is false
                  importLoaders: 1,
                  localIdentName: "[name]--[local]--[hash:base64:8]"
                }},
            'sass-loader'
        ],
    },
    {
        test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|ttf|eot)(\?.*$|$)/,
        use: ["file-loader"]
    }],
  },
  resolve: {
    modules: ["node_modules", path.resolve('node_modules')]
  }
};


module.exports = cfg;