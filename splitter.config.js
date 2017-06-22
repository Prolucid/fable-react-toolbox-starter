const fableSplitter = require("fable-splitter").default;

const babelOptions = {
  // -- add this to generate UMD modules
  // plugins: [
  //   ["transform-es2015-modules-umd"],
  // ],
  // -- or add this to transpile to ES5
  // presets: [
  //   ["es2015", { modules: "umd" }],
  // ],
  // -- add this to generate source maps
  // sourceMaps: true,
  // etc.
};

const fableOptions = {
  // fableCore: "./node_modules/fable-core",
  // plugins: [],
  // define: [],
  // etc.
};

const prepackOptions = {
  // etc.
};

const options = {
  entry: "./Fable.ReactToolbox.Starter.fsproj",
  outDir: "./out",
  // port: 61225,
  babel: babelOptions,
  fable: fableOptions,
  // prepack: prepackOptions, // if added, fable ==> babel ==> prepack
};

fableSplitter(options);