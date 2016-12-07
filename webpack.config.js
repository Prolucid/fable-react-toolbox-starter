var path = require("path");
var webpack = require("webpack");
const autoprefixer = require('autoprefixer');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var cfg = {
  devtool: "source-map",
  entry: "./out/fable-react-toolbox-starter/App.js",
  output: {
    path: path.join(__dirname, "public"),
    publicPath: "/public",
    filename: "bundle.js"
  },
  module: {
    preLoaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: "source-map-loader"
      }
    ],
    loaders: [
	  {
        test: /(\.scss|\.css)$/,
        loader: ExtractTextPlugin.extract('style', 'css?sourceMap&modules&importLoaders=1&localIdentName=[name]__[local]___[hash:base64:5]!postcss!sass')
      }
	]
  },
  resolve: {
    modules: [
      "node_modules", path.resolve("node_modules/")
    ]
  },
  postcss: [autoprefixer],
  sassLoader: {
    data: '@import "theme/_config.scss";',
    includePaths: [path.resolve(__dirname, './out')]
  },
  plugins: [
    new ExtractTextPlugin('bundle.css', { allChunks: true })
  ]
};

if (process.env.WEBPACK_DEV_SERVER) {
    cfg.entry = [
        "webpack-dev-server/client?http://localhost:8081",
        'webpack/hot/only-dev-server',
        "./out"
    ];
    cfg.plugins = [
        new webpack.HotModuleReplacementPlugin()    
    ];
    cfg.module.loaders = [{
        test: /\.js$/,
        exclude: /node_modules/,
        loader: "react-hot-loader"
    }];
    cfg.devServer = {
        hot: true,
        contentBase: "public/",
        publicPath: "/"
    };
}

module.exports = cfg;