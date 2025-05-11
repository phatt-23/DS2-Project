#import "@preview/codelst:2.0.2": sourcecode

// Link Settings
#show link: set text(fill: rgb(0, 0, 100)) // make links blue
#show link: underline // underline links

// Heading Settings
#show heading.where(level: 1): it => {    
  text(2em)[#it.body]
}
#show heading.where(level: 2): set text(size: 1.5em)
#show heading.where(level: 3): set text(size: 1.2em)
#set heading(numbering: "1.")

// Raw Blocks
#set raw(theme: "./theme.tmTheme")
#show raw: set text(font: "Hack Nerd Font", size: 8pt)
#show raw.where(block: true): it => block(
  inset: 8pt,
  radius: 5pt,
  text(it),
  stroke: (
    left: 2pt + luma(230),
  )
)
#show raw.where(block: false): box.with(
  fill: luma(240),
  inset: (x: 3pt, y: 0pt),
  outset: (y: 3pt),
  radius: 2pt,
)

// Font and Language
#set text(
  lang: "cs",
  font: "Liberation Sans", 
  size: 11pt,
)

// Paper Settings
#set page(paper: "us-letter")

// TITLE PAGE begin
#include "title.typ"
// TITLE PAGE end



// Paragraph Settings
#set par(
  justify: true,
  first-line-indent: 1em,
  linebreaks: "optimized",
)

// Text margins
#set block(spacing: 2em)
#set par(leading: 0.8em)

// Start the Page Counter
#counter(page).update(1)

// #v(8em)
// #include "outline.typ"

#pagebreak()


= Relational Model 

#align(horizon)[
  #figure(
    image("./conceptual_relational_model.svg"),
    gap: 6em,
  )
]

#pagebreak();

#set page(flipped: true)

= Form Design 

#emph(text(orange)[ Database functions (functions touching the database) ])

#emph(text(blue)[ Application functions ])

#emph(text(green)[ Parameters for final transaction function ])

#emph(text(red)[ Transaction function ])

#align(horizon)[
  #figure(
    image("./ds2_form_design.png", width: 64em),
    gap: 6em,
  )
]

#set page(flipped: false)


#pagebreak()

= Functions

#let functions_script = read("./functions.go")

#sourcecode[
  #raw(functions_script, lang: "go")
]

#pagebreak();

= Transaction 

#let transaction_script = read("./transaction.go")

#sourcecode[
  #raw(transaction_script, lang: "go")
]

