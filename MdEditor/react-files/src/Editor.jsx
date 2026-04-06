
import Prism from "prismjs";
import "prismjs/themes/prism.css";

import "prismjs/components/prism-javascript";
import "prismjs/components/prism-python";
import "prismjs/components/prism-css";

window.Prism = Prism;
import { useState } from "react";
import {
  MDXEditor,

  // Plugins
  headingsPlugin,
  diffSourcePlugin,
  listsPlugin,
  quotePlugin,
  thematicBreakPlugin,
  markdownShortcutPlugin,
  linkPlugin,
  imagePlugin,
  tablePlugin,
  codeBlockPlugin,

  // Toolbar
  toolbarPlugin,
  UndoRedo,
  BoldItalicUnderlineToggles,
  CodeToggle,
  CreateLink,
  InsertImage,
  InsertTable,
  ListsToggle,
  BlockTypeSelect,
  Separator,
  InsertThematicBreak,
  InsertCodeBlock,

  DiffSourceToggleWrapper

} from "@mdxeditor/editor";

import "@mdxeditor/editor/style.css";
import "./editor.css";

export default function Editor() {
  const [content, setContent] = useState(`# Welcome to MDX Editor Advanced!


`);




const handleDownload = () => {
  const blob = new Blob([content], { type: "text/markdown;charset=utf-8" });
  const url = URL.createObjectURL(blob);

  const a = document.createElement("a");
  a.href = url;
  a.download = "document.md";
  a.click();

  URL.revokeObjectURL(url);
};

  return (
    <div className="app-container">
      <h1  className="title">MDX Editor Advanced</h1>
      <h3 className="credit">By <a href="https://github.com/BSdeployment" target="_blank" rel="noopener noreferrer">
       Bs DotNet
      </a></h3>

      <div className="editor-wrapper">
        <MDXEditor
          markdown={content}
          onChange={setContent}
          plugins={[
            headingsPlugin(),
            listsPlugin(),
            quotePlugin(),              // ✔️ ציטוטים
            thematicBreakPlugin(),      // ✔️ קו מפריד
            markdownShortcutPlugin(),
            linkPlugin(),
            imagePlugin(),
            tablePlugin(),
            diffSourcePlugin(),
            codeBlockPlugin({
              defaultCodeBlockLanguage: "js"
            }),

            toolbarPlugin({
              toolbarContents: () => (
                <DiffSourceToggleWrapper>
                  <>
                    <UndoRedo />
                    <Separator />

                    <BoldItalicUnderlineToggles />
                    <CodeToggle />
                    <Separator />

                    <BlockTypeSelect />
                    <Separator />

                    <ListsToggle />
                    <Separator />

                    

                    <Separator />

                    <CreateLink />
                    <InsertImage />
                    <InsertTable />
                    <Separator />

                    <InsertThematicBreak />
                    <Separator />

                    <InsertCodeBlock />
                   
                  </>
                </DiffSourceToggleWrapper>
              )
            })
          ]}
        />
      </div>
            <button className="floating-save-btn" onClick={handleDownload}>
        💾
        </button>

      {/* <div className="preview">
        <h2>Markdown Output:</h2>
        <pre>{content}</pre>
      </div> */}
    </div>
  );
}