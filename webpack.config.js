var path = require("path");
var webpack = require("webpack");
const autoprefixer = require('autoprefixer');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

var babelOptions = {
  presets: [["es2015", {"modules": false}]],
  plugins: ["transform-runtime"]
}

module.exports = {
  devtool: "source-map",
  entry: resolve('./Fable.ReactToolbox.Starter.fsproj'),
  output: {
    filename: 'bundle.js',
    path: resolve('./public'),
  },
  devServer: {
    contentBase: resolve('./public'),
    port: 8080
 },
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: { babel: babelOptions }
        }
      },
      {
        test: /\.js$/,
        exclude: /node_modules[\\\/](?!fable-)/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        },
      },
      {
        test: /(\.scss|\.css)$/,
        loader: ExtractTextPlugin.extract({
          loader: [
            {
              loader: 'css-loader',
              query: {
                modules: true,
                sourceMap: true,
                importLoaders: true,
                localIdentName: '[name]__[local]___[hash:base64:5]'
              }
            },
            {
              loader: 'postcss-loader',
              query: {
                plugins: () => [
                  autoprefixer
                ]
              }
            },
            {
              loader: 'sass-loader',
              query: {
                data: '@import "theme/_config.scss";',
                includePaths: [path.resolve(__dirname, 'theme')]
              }
            }
          ]
        }),
      }
    ]
  },
  resolve: {
    modules: ["node_modules", path.resolve(__dirname, '.')]
  },
  plugins: [
    new ExtractTextPlugin({ filename: 'bundle.css', allChunks: true })
  ]
};
