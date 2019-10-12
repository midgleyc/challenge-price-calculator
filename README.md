The aim of this challenge was to produce a price calculator for a shopping basket of goods, applying special offers as applicable; input to a console app which outputs results in a particular format. This README documents the assumptions I made about unspecified requirements, and some of the reasons behind the design architecture.

# Unspecified requirements and assumptions made

## Input

You could choose to accept aliases for products -- e.g. allowing both "Apple" and "Apples" for apples in the example given. I decided not to: for one, I control the public interface, and I can demand that all callers use the correct identifiers for items. For two, it's possible that "Apple" and "Apples" could refer to different products in the future -- there's no need to constrain the space of identifiers too early. I think it's better to keep the interface simple, with no "magic" behaviour.

What exactly to do in the case of invalid input was unspecified. I identified two main possibilities: 
* parse as much as possible, note the ones you failed to parse, and then continue with the ones that are correct
* parse as much as possible, note the ones you failed to parse, then abort, instructing the caller to get it right next time
I think the latter is better -- folk don't necessarily check log messages, and misspelling a high-ticket item could lead to a substantially different output than desired. It's more important to get the right answer than to get any answer. You could also abort on the first error, but I think it's kinder to do as much as possible (even though this is slightly slower to return).

The behaviour on the case of an empty basket was unspecified. I decided to treat it as valid.

## Output

The output in case of multiple applications of the same discount was unspecified. For example, suppose you buy three apples, and apples are 10% off. Should the discount portion of the output be:
* 3x Apples 10% off: -30p
* Apples 10% off: -30p
* Apples 10% off: -10p
  Apples 10% off: -10p
  Apples 10% off: -10p
* something else entirely?
I decided to duplicate the discount -- that seems to be the most common behaviour on receipts.

## Offer logic

Can two offers apply to the same item type? This isn't currently a requirement, and I've left it as undefined behaviour. If you require that they can, the complexity of the solution increases, possibly quite drastically.

Explicitly requiring that they can't is the simpler solution: tag each offer with metadata about which items it affects, add a test which runs through the existing offers and fails in the case that two separate offers apply to the same item.

Suppose that they can. In the case of a 10% off discount on apples and a "buy 2 bananas, get an apple half off" offer, it seems sensible that these should stack. In this case, a fairly simple design is to either update the price of items while iterating over them, or to add a "discount multiplier" to items. The former is better in cases where you expect offers like "Buy three bags of crisps for £3" (as there's no mulitiplier involved there), and the latter in cases where you expect that knowing the original price of the item would be useful (for example, if you expect offers to arrive that don't stack with each other).

Certain offers are less clear about whether they should stack. Suppose you get bread half off for buying two apples, or for buying two jars of jam. If you buy two jam, two apples and one bread, should the bread be one quarter of the original price? If so, if you buy two jam, two apples and two bread loaves, should it favour one bread at full and one at one quarter, or two bread both at halves? The latter is more sensible, and requires an awareness at the item level of which offers are applied.

I would expect a comprehensive solution to require information about:
* which offers stack with which other offers
* which order offers are applied in (important for "percentage" vs "absolute" deals)

As we can't know what the intended behaviour is for offers, I've left it as a simple solution that can be extended into whichever desired state depending on the actual requirements.

## Input of inventory and offers

How should inventory and offers be put into the system? For now, they're hardcoded as part of the Library.PriceCalculator.Resources assembly.

Inventory is quite simple -- each item is an identifier combined with a price. Possible extensions would be prices in non-UK currencies, but for now let's consider only the simple case. I think a good implementation for this would be to read from a JSON file: it could contain an array containing the inventory items, specifying their identifier and price.

Offers are substantially more complicated. I think a simple solution would be to provide an "OfferFactory" in Resources that accepts an identifier and returns an Offer, and then read from a configuration file the identifiers of offers that are currently active. This requires that all offers be present in the OfferFactory: you could acquire a bit more freedom by adding some abstraction -- if there's a class for "10% off apples", you know what the class for "20% off bread" looks like -- but going down this road would eventually want you to implement some sort of DSL. 
